const maxRange = 60.0;
const localPlayer = mp.players.local;
let musicPlayer = mp.browsers.new('package://player/musicPlayer/index.html');
let id = 0;
let activeSounds = {}, soundInterval;

const soundManager = {
    add: function (player, id) {
        if (player && activeSounds[id]) {
            activeSounds[id].listeners.push(player);
            if (localPlayer.handle !== player.handle) return mp.events.callRemote('addListener', player.id, JSON.stringify(activeSounds[id]));
            if (musicPlayer) musicPlayer.execute(`playAudio("${id}", "${activeSounds[id].url}", "${activeSounds[id].volume}")`);
        }
    },
    remove: function (player, id) {
        if (activeSounds[id]) {
            let idx = activeSounds[id].listeners.indexOf(player);
            if (idx !== -1) {
                activeSounds[id].listeners.splice(idx, 1);
                if (localPlayer.handle !== player.handle) return mp.events.callRemote('removeListener', player.id, id);
                if (musicPlayer) musicPlayer.execute(`stopAudio("${id}")`);
            }
        }
    },
    setVolume: function (player, id, volume) {
        if (activeSounds[id]) {
            if (localPlayer.handle !== player.handle) return mp.events.callRemote('changeSoundVolume', player.id, id, volume);
            if (musicPlayer) musicPlayer.execute(`setVolume("${id}", "${volume}")`);
        }
    },
    pauseToggle: function (player, id, pause) {
        if (activeSounds[id] && activeSounds[id].listeners.includes(player)) {
            activeSounds[id].paused = pause;
            if (localPlayer.handle !== player.handle) return mp.events.callRemote('pauseToggleSound', player.id, id, pause);
            if (musicPlayer) musicPlayer.execute(`setPaused("${id}", "${pause}")`);
        }
    }
}

mp.events.add({
    'createSound': (soundObj) => {
        soundObj = JSON.parse(soundObj);
        activeSounds[soundObj.id] = {
            id: soundObj.id,
            url: soundObj.url,
            pos: soundObj.pos,
            volume: soundObj.volume,
            range: soundObj.range,
            listeners: soundObj.listeners,
            dimension: soundObj.dimension
        }
    },
    'soundPosition': (id, pos) => {
        pos = JSON.parse(pos);
        if (activeSounds[id]) { 
            activeSounds[id].pos = pos;
        }
    },
    'soundRange': (id, range) => {
        if (activeSounds[id]) { 
            activeSounds[id].range = range;
        }
    },
    'setSoundVolume': (id, volume) => {
        mp.game.audio.setSoundVolume(id, volume)
    },
    'destroySound': (soundID) => {
        if (activeSounds[soundID]) {
            soundManager.remove(localPlayer, soundID);
            activeSounds[soundID] = null;
        }
        if (Object.keys(activeSounds).length < 1) {
            if (soundInterval) clearInterval(soundInterval);
        }
    },
    'pauseSound': (soundID) => {
        setSoundPause(soundID);
    },
    'resumeSound': (soundID) => {
        setSoundResume(soundID);
    },
    'audioFinish': (soundID) => {
        mp.events.call('destroySound', soundID);
        mp.events.callRemote('soundFinish', soundID);
    },
    'audioError': (soundID, err) => {
        mp.events.call('destroySound', soundID);
        mp.events.callRemote('soundError', soundID, err);
    }
});

mp.game.audio.playSound3D = function (url, pos, range = maxRange, volume = 1, dimension = 0) {
    id += 1;
    activeSounds[id] = {
        id: id,
        url: url,
        pos: pos,
        volume: volume,
        range: range,
        dimension: dimension,
        listeners: [],
        paused: false,
    };
    activeSounds[id].destroy = function () {
        return mp.events.callRemote('soundState', 'destroySound', this.id);
    };
    activeSounds[id].pause = function () {
        return mp.events.callRemote('soundState', 'pauseSound', this.id);
    };
    activeSounds[id].resume = function () {
        return mp.events.callRemote('soundState', 'resumeSound', this.id)
    };
    if (!soundInterval) initSoundInterval();
    return activeSounds[id];
};

const setSoundResume = function (id) {
    soundManager.pauseToggle(localPlayer, id, false);
};

const setSoundPause = function (id) {
    soundManager.pauseToggle(localPlayer, id, true);
};

mp.game.audio.setSoundVolume = function (id, volume = 1) {
    if (activeSounds[id]) {
        activeSounds[id].volume = volume;
    }
};

mp.game.audio.setSoundPosition = function (id, pos) {
    if (activeSounds[id]) {
        activeSounds[id].pos = pos;
        mp.events.callRemote('soundPosition', id, JSON.stringify(pos));
    }
}

mp.game.audio.setSoundRange = function (id, range) {
    if (range < 0 || range > maxRange) range = maxRange;
    activeSounds[id].range = range;
    mp.events.callRemote('soundRange', id, range);
}

function initSoundInterval () {
    soundInterval = setInterval(() => {
        Object.keys(activeSounds).forEach(sound => {
            if (activeSounds[sound] && !activeSounds[sound].paused) {
                let soundPosition = activeSounds[sound].pos;
                let maxRange = activeSounds[sound].range;
                mp.players.forEach(player => { // I wish there was inRange like in server-side.
                    if (player && player.dimension === activeSounds[sound].dimension && !activeSounds[sound].listeners.includes(player)) {
                        const playerPos = player.position;
                        if (playerPos && soundPosition){
                            let dist = mp.game.system.vdist(playerPos.x, playerPos.y, playerPos.z, soundPosition.x, soundPosition.y, soundPosition.z);
                            if (dist <= maxRange) {
                            soundManager.add(player, sound);
                            }                                                    
                        }
                    }
                });
                
                if(activeSounds[sound].listeners){
                    activeSounds[sound].listeners.forEach(player => {
                    if (player && activeSounds[sound].listeners.includes(player)) {
                        const playerPos = player.position;
                        if (playerPos && soundPosition){
                        let dist = mp.game.system.vdist(playerPos.x, playerPos.y, playerPos.z, soundPosition.x, soundPosition.y, soundPosition.z);
                        if (dist > maxRange || activeSounds[sound].dimension !== player.dimension) {
                            soundManager.remove(player, sound);
                        } else {
                            let volume = (activeSounds[sound].volume - (dist / activeSounds[sound].range)).toFixed(2); // Credits to George....
                            soundManager.setVolume(player, sound, volume);
                        }
                        }
                    }
                });
                }                
            }
        });
    }, 500);
};
