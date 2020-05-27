require('./axe/creador/creador.js');
require('./axe/creador/selector.js');
require('mp-commands');
require('./axe/keys.js');
require('./axe/menu/index.js');
require('./axe/checkpoints.js')
require('character/animations/anim.js');
require('hud/index.js');
require('hud/info.js');
require('hud/indexon.js');
require('scaleform_messages/index.js');
require('./scaleformHud');
//require('charcreator');

require('crouch/index.js');
//require('weapondisplay/index.js');
//require('static-attachments/index.js');

require("./ropa/index.js");
require("./ropa/ropa.js");
require('./axe/npc/cajas.js');
require('binds.js');
require('notifications.js');
require('voice.js');
require('fly.js');
require('doorsPD.js');
require('death.js');
require('attachs.js');
require('./axe/velocimetro/index.js');
require('./player'); 
require('./axe/npc/index.js');
require('./axe/apuntar.js');
require('boombox.js');

require('./axe/trabajos/pesca.js');
require('./axe/caraudio/index.js');
require('./axe/rentacar.js');
require('./axe/skycam.js');


mp.gui.chat.show(false);
//let chatbox = mp.browsers.new('package://chat_old/index.html');
//chatbox.markAsChat();
//chatbox.execute('show()');

// Modelos
mp.game.streaming.requestModel(mp.game.joaat('prop_amb_phone'));

mp.game.gxt.set('PM_PAUSE_HDR', 'Hyaxe RP');
mp.game.gxt.set('PM_MP_NO_JOB', 'Servidor roleplay en RAGE Multiplayer');

// Nametags
const maxDistance = 25*25;
const width = 0.03;
const height = 0.0065;
const border = 0.001;
const color = [255,255,255,255];

mp.nametags.enabled = false;

mp.events.add('render', (nametags) =>
{
	// Estamina
	if(mp.players.local.isSprinting())
	{
		mp.game.player.restoreStamina(100);
	}
	
	// F y G
	const controls = mp.game.controls;
	
	controls.enableControlAction(0, 23, true);
	controls.disableControlAction(0, 58, true);
	
	if(controls.isDisabledControlJustPressed(0, 58))
	{
		let position = mp.players.local.position;		
		let vehHandle = mp.game.vehicle.getClosestVehicle(position.x, position.y, position.z, 5, 0, 70);
		
		let vehicle = mp.vehicles.atHandle(vehHandle);
		
		if(vehicle
			&& vehicle.isAnySeatEmpty()
			&& vehicle.getSpeed() < 5)
		{
			mp.players.local.taskEnterVehicle(vehicle.handle, 5000, 0, 2, 1, 0);
		}
	}

	// Nametags
	const graphics = mp.game.graphics;
    const screenRes = graphics.getScreenResolution(0, 0);
	
    nametags.forEach(nametag => {
        let [player, x, y, distance] = nametag;
		
        if(distance <= maxDistance) {	   
            let scale = (distance / maxDistance);
            if(scale < 0.6) scale = 0.6;
			
            var health = player.getHealth();
            health = health < 100 ? 0 : ((health - 100) / 100);
		   
            var armour = player.getArmour() / 100;
			
            y -= scale * (0.005 * (screenRes.y / 1080));
			
            if(player.getVariable("MICRO_STATUS")){
				mp.game.graphics.drawText("~b~" + player.name + " (" + player.remoteId + ")", [x, y],
				{
					font: 4,
					color: [255, 255, 255, 255],
					scale: [0.4, 0.4],
					outline: true
				});
			}
			else{
				mp.game.graphics.drawText(player.name + " (" + player.remoteId + ")", [x, y],
				{
					font: 4,
					color: [255, 255, 255, 255],
					scale: [0.4, 0.4],
					outline: true
				});
			}
        }
	});
});