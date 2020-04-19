require('character/creator.js');
require('character/selector.js');
require('character/animations/anim.js');
require('hud/index.js');
require('scaleform_messages/index.js');
require('./scaleformHud');

require('binds.js');
require('notifications.js');
require('voice.js');
require('fly.js');
require('doorsPD.js');
require('death.js');
require('attachs.js');
require('./axe/velocimetro_axe/index.js');

mp.gui.chat.show(false);
let chatbox = mp.browsers.new('package://chat_old/index.html');
chatbox.markAsChat();
chatbox.execute('show()');

mp.game.vehicle.defaultEngineBehaviour = false;
mp.game.player.restoreStamina(1.0);

//mp.game.gxt.set('PM_PAUSE_HDR', 'Condado Zero');
//mp.game.gxt.set('PM_MP_NO_JOB', 'Servidor roleplay en RAGE Multiplayer');