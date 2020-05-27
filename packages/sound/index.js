var activeSounds = [];

mp.events.add({
    'soundState': (player, state, soundID) => {
        mp.players.call(state, [soundID]);
    },
    'syncSound': (player, soundData) => {
        mp.players.call(mp.players.toArray().filter(p.id !== player.id), 'createSound', [soundData]);
    },
    'changeSoundVolume': (player, listenerID, soundID, volume) => {
        let listener = mp.players.at(listenerID);
        listener.call('createSound', [soundID, volume]);
    },
    'addListener': (player, listenerID, soundData) => {
        let listener = mp.players.at(listenerID);
        listener.call('createSound', [soundData]);
    },
    'removeListener': (player, listenerID, soundID) => {
        let listener = mp.players.at(listenerID);
        listener.call('destroySound', [soundID]);
    },
    'soundPosition': (soundID, pos) => {
        mp.players.call('soundPosition', [soundID, pos]);
    },
    'soundRange': (soundID, range) => {
        mp.players.call('soundRange', [soundID, range]);
    },
    'soundFinish': (soundID) => {
        if (activeSounds[soundID]) {
            activeSounds[soundID] = null;
        }
    },
    'soundError': (soundID, err) => {
        if (activeSounds[soundID]) {
            err = JSON.parse(err);
            activeSounds[soundID] = null;
            console.log(`[ERR] ${err.code}: ${err.error}`);
        }
    }
});