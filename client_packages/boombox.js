//require('./player'); // sound 3d api
let player = mp.players.local;
let sound = null;

mp.events.add('startBoombox', (type) => {
    if(sound != null) sound.destroy();
    // 1 y 2 hip hop, 3 regeton, 4 trap, 5 y 6 EDM, 7 y 8 techno
    switch(type){
        case 1:
             sound = mp.game.audio.playSound3D('http://us3.internet-radio.com:8313/listen.pls&t=.m3u', player.position, 20, 1, 0);
         break;
         case 2:
             sound = mp.game.audio.playSound3D('http://uk7.internet-radio.com:8078/listen.pls&t=.m3u', player.position, 20, 1, 0);
         break;
         case 3:
             sound = mp.game.audio.playSound3D('http://us1.internet-radio.com:8070/listen.pls&t=.m3u', player.position, 20, 1, 0);
         break;
         case 4:
             sound = mp.game.audio.playSound3D('http://us3.internet-radio.com:8026/live.m3u&t=.m3u', player.position, 20, 1, 0);
         break;
         case 5:
             sound = mp.game.audio.playSound3D('http://uk4.internet-radio.com:8049/listen.pls&t=.m3u', player.position, 20, 1, 0);
         break;
         case 6:
             sound = mp.game.audio.playSound3D('http://uk5.internet-radio.com:8083/live.m3u&t=.m3u', player.position, 20, 1, 0);
         break;
         case 7:
             sound = mp.game.audio.playSound3D('http://uk7.internet-radio.com:8000/listen.pls&t=.m3u', player.position, 20, 1, 0);
         break;
         case 8:
             sound = mp.game.audio.playSound3D('http://us3.internet-radio.com:8352/listen.pls&t=.m3u', player.position, 20, 1, 0);
         break;
            }
});

mp.events.add('destroyBoombox', () => {
    sound.destroy();
});