const instructions = require('betterInstructions/better_instructions');

mp.game.audio.setAudioFlag("LoadMPData", true);

mp.events.add('NotificationSound', () => {
  mp.game.audio.playSoundFrontend(-1, "Pin_Good", "DLC_HEIST_BIOLAB_PREP_HACKING_SOUNDS", true);
});

mp.events.add('chat_goal', (texto, texto2) => {
  mp.game.ui.messages.showShard(texto, texto2, 0, 11);
  mp.game.audio.playSoundFrontend(-1, "MP_WAVE_COMPLETE", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
});

mp.events.add('testPaydayLevel', (texto) => {
  mp.banners.createObjective(2, "Payday", texto).then(done => { });
});

mp.events.add('displayBusinessVenta', () => {
  mp.game.audio.playSoundFrontend(-1, 'Event_Message_Purple', 'GTAO_FM_Events_Soundset', true);
  const horizontalInstructionList = new instructions(-1);

  horizontalInstructionList.addButton('Negocio en venta, para mas información presiona ', 'F6');

  if(!horizontalInstructionList.isActive()) {
    horizontalInstructionList.toggleHud(true);
  }

  setTimeout( () => {
    if(horizontalInstructionList.isActive()) {
      horizontalInstructionList.toggleHud(false);
    }
  }, 5000);
});

mp.events.add('adviceBuyVehicle', () => {
  mp.game.audio.playSoundFrontend(-1, 'Event_Message_Purple', 'GTAO_FM_Events_Soundset', true);
  const horizontalInstructionList = new instructions(-1);

  horizontalInstructionList.addButton('Para comprar un vehículo, súbete a el pulsando la tecla ', 'F');

  if(!horizontalInstructionList.isActive()) {
    horizontalInstructionList.toggleHud(true);
  }

  setTimeout( () => {
    if(horizontalInstructionList.isActive()) {
      horizontalInstructionList.toggleHud(false);
    }
  }, 5000);
});

mp.events.add('tipEngineVehicle', () => {
  mp.game.audio.playSoundFrontend(-1, 'Event_Message_Purple', 'GTAO_FM_Events_Soundset', true);
  const horizontalInstructionList = new instructions(-1);

  horizontalInstructionList.addButton('Para encender el motor del vehículo pulsa la tecla ', 'N');

  if(!horizontalInstructionList.isActive()) {
    horizontalInstructionList.toggleHud(true);
  }

  setTimeout( () => {
    if(horizontalInstructionList.isActive()) {
      horizontalInstructionList.toggleHud(false);
    }
  }, 5000);
});

mp.events.add('tipMenuEmpresa', () => {
  mp.game.audio.playSoundFrontend(-1, 'Event_Message_Purple', 'GTAO_FM_Events_Soundset', true);
  const horizontalInstructionList = new instructions(-1);

  horizontalInstructionList.addButton('Para abrir el menú de tu empresa pulsa ', 'F6');

  if(!horizontalInstructionList.isActive()) {
    horizontalInstructionList.toggleHud(true);
  }

  setTimeout( () => {
    if(horizontalInstructionList.isActive()) {
      horizontalInstructionList.toggleHud(false);
    }
  }, 5000);
});

mp.events.add('tipComprarCasa', () => {
  mp.game.audio.playSoundFrontend(-1, 'Event_Message_Purple', 'GTAO_FM_Events_Soundset', true);
  const horizontalInstructionList = new instructions(-1);

  horizontalInstructionList.addButton('Para acceder el menú de interacción pulsa', 'F5');

  if(!horizontalInstructionList.isActive()) {
    horizontalInstructionList.toggleHud(true);
  }

  setTimeout( () => {
    if(horizontalInstructionList.isActive()) {
      horizontalInstructionList.toggleHud(false);
    }
  }, 5000);
});

mp.events.add('tipOwnerCasa', () => {
  mp.game.audio.playSoundFrontend(-1, 'Event_Message_Purple', 'GTAO_FM_Events_Soundset', true);
  const horizontalInstructionList = new instructions(-1);

  horizontalInstructionList.addButton('Para acceder el menú de propiedad pulsa', 'F6');

  if(!horizontalInstructionList.isActive()) {
    horizontalInstructionList.toggleHud(true);
  }

  setTimeout( () => {
    if(horizontalInstructionList.isActive()) {
      horizontalInstructionList.toggleHud(false);
    }
  }, 5000);
});

var text = "";
var NotifyText = false;

mp.events.add('chat_notification', (texto) => {
  NotifyText = true;
  text = texto;
});

mp.events.add('stop_chat_notification', () => {
  NotifyText = false;
});

  mp.events.add('render', () => {
    if(NotifyText){
      mp.game.graphics.drawText(text, [0.5, 0.9], {
        font: 0,
        color: [255, 255, 255, 185],
        scale: [0.4, 0.4],
        outline: true
      });
    }
  });

