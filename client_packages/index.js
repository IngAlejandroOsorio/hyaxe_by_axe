//require('./axe/creador/selector.js');
require('character/animations/anim.js');
require('hud/index.js');
require('hud/info.js');
require('hud/indexon.js');
require('scaleform_messages/index.js');
require('./scaleformHud');
//require('charcreator');
require('./axe/creador/creador.js');

require('crouch/index.js');
//require('weapondisplay/index.js');
//require('static-attachments/index.js');

require("./ropa/index.js");
require("./ropa/ropa.js");

require('binds.js');
require('notifications.js');
require('voice.js');
require('fly.js');
require('doorsPD.js');
require('death.js');
require('attachs.js');
require('./axe/velocimetro/index.js');

//mp.gui.chat.show(false);
//let chatbox = mp.browsers.new('package://chat_old/index.html');
//chatbox.markAsChat();
//chatbox.execute('show()');

mp.game.gxt.set('PM_PAUSE_HDR', 'Hyaxe RP');
mp.game.gxt.set('PM_MP_NO_JOB', 'Servidor roleplay en RAGE Multiplayer');

mp.events.add('render', () =>
{
	if(mp.players.local.isSprinting())
	{
		mp.game.player.restoreStamina(100);
	}
	
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
});


