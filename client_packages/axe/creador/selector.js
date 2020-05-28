let browser;
let characters;
var chajson;

mp.events.add("transicion", (tiempo) => {
  tiempo = parseInt(tiempo);
  mp.game.cam.doScreenFadeOut(100);   
  setTimeout(() => {
    mp.game.cam.doScreenFadeIn(tiempo);
    mp.game.invoke("0x31B73D1EA9F01DA2");
  }, tiempo);  
});


mp.events.add("UpdateCharactersList", (data) => {
  if (!browser) {
    browser = mp.browsers.new("package://statics/pj/selector.html");
  }

  characters = data;
  var character = JSON.parse(characters);  
  setTimeout(() => {
    mp.gui.cursor.visible = true;
    browser.execute(`updateList(${data})`); 
    browser.execute(`changetext('${character[0].name}')`);
  }, 500);
  mp.events.callRemote("playerLoadSelector", character[0].name);

  mp.gui.cursor.visible = true;

  mp.players.local.position = new mp.Vector3(-2633.1172, 1875.4899, 160.13458);
  mp.players.local.freezePosition(true);
  mp.events.callRemote("SetPlayerRot", 132.30782);

  var start_camera = mp.cameras.new("start", new mp.Vector3(-2642.427978515625, 1873.5687255859375, 164.0581512451172), new mp.Vector3(0, 0, -30.0), 60.0);
  start_camera.pointAtCoord(-2650.219482421875, 1872.9462890625, 161.7822265625);
  start_camera.setActive(true);
  mp.game.cam.renderScriptCams(true, false, 0, true, false);
//-2639.25390625, 1870.9058837890625, 159.90162658691406
//-2635.5693359375, 1873.7939453125, 159.1344757080078
  var end_camera = mp.cameras.new("end", new mp.Vector3(-2637.28662109375, 1872.0269775390625, 161.44723510742188), new mp.Vector3(0, 0, -30), 60.0);
  end_camera.pointAtCoord(-2633.343505859375, 1875.396484375, 159.22457885742188);
  end_camera.setActiveWithInterp(start_camera.handle, 4000, 0, 0);


});



mp.events.add("SelectCharacterToPlay", (id) => {
  if (browser) {
    browser.destroy();
    browser = undefined;
  }
  mp.events.call("transicion",1500);
  setTimeout(function(){ 
    mp.gui.cursor.visible = false;
    mp.game.cam.renderScriptCams(false, false, 0, true, false);
  }, 500);

  var player = mp.players.local;

  mp.events.call('moveSkyCamera', player, 'up', 1, false);

    // After 5 seconds, camera start to go back to player.
  setTimeout(() => {
      //player.position = new mp.Vector3(0,0,10); // Set your position if you want
      mp.events.call('moveSkyCamera', player, 'down');
  }, 5000);
  
  const character = JSON.parse(characters);
  //mp.events.callRemote("posjss", "llamo char id: "+character[id].id);
  mp.events.callRemote("SelectCharacter", character[id].id);
});



mp.events.add("animFinCreador", () => {

  mp.events.call("transicion",1500);
  var player = mp.players.local;

  mp.events.call('moveSkyCamera', player, 'up', 1, false);
    
  setTimeout(() => {
      mp.events.call('moveSkyCamera', player, 'down');
  }, 5000);

});

mp.events.add('alphaJugador', (modo) => {
  if (modo){
    setTimeout(() => { mp.players.local.setAlpha(0)}, 500);
  }else{
    setTimeout(() => { mp.players.local.setAlpha(255)}, 500);
  }
  
});



mp.events.add('ApplyCharacterFeatures', (i) => {
  var character = JSON.parse(characters);
  mp.events.callRemote("playerLoadSelector", character[i].name);
});

mp.events.add('PonerNombre', (nombre) => {
  mp.players.local.name = nombre;
});


mp.events.add('DeleteCharacter', (id) => {
  var character = JSON.parse(characters);
  mp.events.callRemote("DeleteCharacter", character[id].id);
});

mp.events.add("SendToCharacterCreator", () => {
  if (browser) {
    browser.destroy();
    browser = undefined;
  }
  mp.events.call("transicion",1500);
  mp.events.callLocal("CreatePjAlphaEvent");
});
