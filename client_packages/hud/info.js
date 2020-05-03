var playerid = 0;
var playersonline = 0;
var voz = false;
var isloged = false;

mp.events.add('update_hud_players', (players) => {
	playersonline = players;	
});

mp.events.add('update_hud_player', (id) => {
    playerid = id;
    isloged = true;
});

mp.events.add('update_hud_microphone', (microphone) => {
    if(microphone == 0) voz = false;
    else voz = true;
});

mp.events.add("render", () => {
    if(isloged){
        mp.game.graphics.drawText("~r~" + playersonline + " ~w~jugadores en linea", [0.2108345478773117, 0.80859375], {
            font: 4,
            color: [255,255,255,255],
            scale: [1.0,0.50],
            outline: false
        });
    
        if(voz){
            mp.game.graphics.drawText("~g~Voz activada", [0.1923177126646042, 0.83984375], {
                font: 4,
                color: [255,255,255,255],
                scale: [1.0,0.50],
                outline: false
            });
        }else{
            mp.game.graphics.drawText("~r~Voz desactivada", [0.2013177126646042, 0.83984375], {
                font: 4,
                color: [255,255,255,255],
                scale: [1.0,0.50],
                outline: false
            });
        }
    
        mp.game.graphics.drawText("ID: ~r~" + playerid, [0.1786237210035324, 0.8684895634651184], {
            font: 4,
            color: [255,255,255,255],
            scale: [1.0,0.50],
            outline: false
        });
    
        mp.game.graphics.drawText("Hyaxe Alpha (v.hyaxe.com)", [0.9165446758270264, 0.96875], {
            font: 0,
            color: [255,255,255,255],
            scale: [1.0,0.40],
            outline: false
        });
    }
});