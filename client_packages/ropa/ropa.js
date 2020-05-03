

var character_data = {
  name: "",
  gender: 0,
  torso: 0,
  legs: 1,
  feet: 1,
  undershirt: 57,
  topshirt: 1,
  topshirtTexture: 0,
  accessory: 0,
  hairType: 1,
  hairColor: 0,
  hairHighlight: 0,
  beard: 0
};

var ropaInicial = [];

var precio = 0;
var precio_last = 0;
var slot_last = 0;
var camx = null;
//beard beardColor makeupColor lipstick lipstickColor hairType hairColor hairHighlight 
mp.events.add("AbrirTiendaRopa", (genero, tienda, torso, topshirt, topshirtTexture, undershirt, legs, feet, price, o_id, beard, beardColor, makeup, makeupColor, lipstick, lipstickColor, hairType, hairColor, hairHighlight, owner_name) => { // Aqui inicia todo desde el colshape.
	
	resetCharacterRopa(torso, topshirt, topshirtTexture, undershirt, legs, feet, beard, beardColor, makeup, makeupColor, lipstick, lipstickColor, hairType, hairColor, hairHighlight);
	
	let xx = 2.5//5.0;
	let yy = 7.5//15.0;
	let zz = 1.2;

	mp.game.cam.destroyAllCams(true);
	let plaPos = mp.players.local.position;
	let vecPos = mp.players.local.getForwardVector();
	let plaRot = mp.players.local.getRotation(2);
	//mp.gui.chat.push("camara try1: "+ plaPos.x +" "+ plaPos.y +" "+ plaPos.z + " pos final: "+ plaPos.x + 5.0 +" "+ plaPos.y + 15.0 +" "+ plaPos.z+1.2 + " rotation: "+plaRot.x+" "+plaRot.y+" "+plaRot.z);
	camx = mp.cameras.new('default', new mp.Vector3(plaPos.x + parseFloat(xx), plaPos.y + parseFloat(yy), plaPos.z + parseFloat(zz)), new mp.Vector3(0,0,0), 40);
	camx.pointAtCoord(plaPos.x - parseFloat(xx), plaPos.y - parseFloat(yy), plaPos.z - parseFloat(zz));
	camx.setActive(true);	
	//camx.pointAtPedBone(mp.players.local.handle, 10706, 0, 0, 0, true);	
	mp.game.cam.renderScriptCams(true, false, 20000, true, false);

	//mp.gui.chat.push("Abri tienda de ropa");
	mp.gui.cursor.visible = true;
	mp.gui.chat.activate(false);
	if (tienda == "ponsoboys"){
		browserRopa = mp.browsers.new("package://ropa/ponsoboys.html");	
	}else if(tienda == "binco"){
		browserRopa = mp.browsers.new("package://ropa/binco.html");
	}else{
		browserRopa = mp.browsers.new("package://ropa/peluqueria.html");
	}
	
	let issale = "";
	browserRopa.execute('iniciar("'+genero+'","'+issale+'");'); 	
});

mp.events.add("camp", (x,y,z) => {

	camx.pointAtCoord(parseFloat(x), parseFloat(y), parseFloat(z));

})

mp.events.add("camx", (x,y,z,rx,ry,rz) => {

	camx.setCoord(parseFloat(x), parseFloat(y), parseFloat(z));
	camx.setRot(parseFloat(rx), parseFloat(ry), parseFloat(rz), 2);

})

mp.events.add("camh", () => {
    let playerPosition = mp.players.local.position
    
    camx.setActive(true);
    camx.pointAtPedBone(mp.players.local.handle, 57005, 0, 0, 0, true);
    camx.setCoord(playerPosition.x + 1, playerPosition.y + 1, playerPosition.z);
    mp.game.cam.renderScriptCams(true, false, 0, true, false);
});

mp.events.add("camb", (x,y,z,bone) => {
    let playerPosition = mp.players.local.position
    
    camx.setActive(true);
    camx.pointAtPedBone(mp.players.local.handle, parseInt(bone), parseFloat(x), parseFloat(y), parseFloat(z), true);
    //camx.setCoord(parseFloat(x), parseFloat(y), parseFloat(z));
    mp.game.cam.renderScriptCams(true, false, 0, true, false);
});


function resetCharacterRopa(torso, topshirt, topshirtTexture, undershirt, legs, feet, beard, beardColor, makeup, makeupColor, lipstick, lipstickColor, hairType, hairColor, hairHighlight) {
	//mp.gui.chat.push("Seteo ropa de la db: "+parseInt(torso)+parseInt(legs)+parseInt(feet)+parseInt(undershirt)+parseInt(topshirt));
	//beard, beardColor, makeup, makeupColor, lipstick, lipstickColor, hairType, hairColor, hairHighlight
	ropaInicial = [parseInt(torso),parseInt(legs),parseInt(feet),parseInt(undershirt),parseInt(topshirt), parseInt(beard), parseInt(beardColor), parseInt(makeup), parseInt(makeupColor), parseInt(lipstick), parseInt(lipstickColor), parseInt(hairType), parseInt(hairColor), parseInt(hairHighlight)];
  	mp.events.callRemote('SetPlayerClothes', 3, parseInt(torso), 0);
  	mp.events.callRemote('SetPlayerClothes', 4, parseInt(legs), 0);
  	mp.events.callRemote('SetPlayerClothes', 6, parseInt(feet), 0);
  	mp.events.callRemote('SetPlayerClothes', 8, parseInt(undershirt), 0);
  	mp.events.callRemote('SetPlayerClothes', 11, parseInt(topshirt), 0);

  	mp.players.local.setHeadOverlay(1, ropaInicial[5] == null ? 255 : ropaInicial[5], 1, ropaInicial[6], ropaInicial[6]);

  	mp.players.local.setHeadOverlayColor(4, 0, ropaInicial[7], ropaInicial[7]);
  	mp.players.local.setHeadOverlayColor(8, 2, ropaInicial[9], ropaInicial[9]);
}


mp.events.add("CerrarTiendaRopa", () => {
	if (browserRopa) {
    browserRopa.destroy();
	browserRopa = undefined;
	}
	
	mp.events.callRemote('SetPlayerClothes', 3, ropaInicial[0], 0);
  	mp.events.callRemote('SetPlayerClothes', 4, ropaInicial[1], 0);
  	mp.events.callRemote('SetPlayerClothes', 6, ropaInicial[2], 0);
  	mp.events.callRemote('SetPlayerClothes', 8, ropaInicial[3], 0);
  	mp.events.callRemote('SetPlayerClothes', 11, ropaInicial[4], 0);
 
  	mp.players.local.setHeadOverlay(1, ropaInicial[5] == null ? 255 : ropaInicial[5], 1, ropaInicial[6], ropaInicial[6]);

  	mp.players.local.setHeadOverlayColor(4, 0, ropaInicial[7], ropaInicial[7]);
  	mp.players.local.setHeadOverlayColor(8, 2, ropaInicial[9], ropaInicial[9]);


	mp.gui.cursor.visible = false;
	mp.gui.chat.activate(true);
	mp.game.cam.renderScriptCams(false, false, 0, true, false);
	mp.events.callRemote('S_CerrarTiendaData');
});

mp.events.add('ComprarRopaCliente', (precio) => {
  	if (browserRopa) {
    browserRopa.destroy();
	browserRopa = undefined;
	}
	
	mp.gui.cursor.visible = false;
	mp.gui.chat.activate(true);
	mp.game.cam.renderScriptCams(false, false, 0, true, false);
	mp.events.callRemote('ComprarRopa', precio, JSON.stringify(character_data));
});




mp.events.add('MoverCamaraRopa', (pos) => {
  
let plaPos = mp.players.local.position;
  switch (pos) {
    case 0:
      {
        // Head

        camx.setCoord(plaPos.x + 1, plaPos.y + 1, plaPos.z);	
		camx.pointAtPedBone(mp.players.local.handle, 31086, 0, 0, 0, true);
		camx.setActive(true);			
        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
    case 1:
      {
        // Torso
		camx.setCoord(plaPos.x + 3, plaPos.y + 3, plaPos.z + 2);
		camx.pointAtPedBone(mp.players.local.handle, 24816, 0, 0, 0, true);
		camx.setActive(true);			
        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
    case 2:
      {
        // Legs
        camx.setCoord(plaPos.x + 3, plaPos.y + 3, plaPos.z + 2);
		camx.pointAtPedBone(mp.players.local.handle, 11816, 0, 0, 0, true);
		camx.setActive(true);			
        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
    case 3:
      {
        // Feet
        camx.setCoord(plaPos.x + 3, plaPos.y + 3, plaPos.z + 2);
		camx.pointAtPedBone(mp.players.local.handle, 52301, 0, 0, 0, true);
		camx.setActive(true);			
        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
    default:
      {
        // Default
         camx.setCoord(plaPos.x + 3, plaPos.y + 3, plaPos.z + 2);
		camx.pointAtPedBone(mp.players.local.handle, 24816, 0, 0, 0, true);
		camx.setActive(true);			
        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
  }
});

mp.events.add('SelectCharacterComponent', (data) => {
	var mix_data = [
	  0.0,
	  0.1,
	  0.2,
	  0.3,
	  0.4,
	  0.5,
	  0.6,
	  0.7,
	  0.8,
	  0.9
	];
  
	data = JSON.parse(data);
	switch (data.type) {
	  // Face
	  case 13:
		{
		  character_data.faceFirst = data.config1;
		  character_data.faceSecond = data.config2;
		  character_data.faceMix = mix_data[data.config3];
		  mp.players.local.setHeadBlendData(character_data.faceFirst, character_data.faceSecond, 0, character_data.skinFirst, character_data.skinSecond, 0, character_data.faceMix, character_data.skinMix, 0, false);
		  break;
		}
	  // Eyes
	  case 14:
		{
		  character_data.eyeColor = data.config1;
		  mp.players.local.setEyeColor(character_data.eyeColor);
		  break;
		}
	  // Hair
	  case 15:
		{
		  character_data.hairType = data.config1;
		  character_data.hairColor = data.config2;
		  character_data.hairHighlight = data.config3;
		  mp.players.local.setHairColor(character_data.hairColor, character_data.hairHighlight);
		  mp.events.callRemote('SetPlayerClothes', 2, character_data.hairType, 0);
		  break;
		}
	  case 16:
		{
		  if (!character_data.gender) {
			if (data.config1 < 1) {
			  character_data.beard = null;
			}
			else {
			  character_data.beard = data.config1 - 1;
			}
			character_data.beardColor = data.config2;
			mp.players.local.setHeadOverlay(1, character_data.beard == null ? 255 : character_data.beard, 1, character_data.beardColor, character_data.beardColor);
		  }
		  else {
			if (data.config1 < 1) {
			  character_data.lipstick = null;
			}
			else {
			  character_data.lipstick = data.config1 - 1;
			}
			character_data.lipstickColor = data.config2;
			mp.players.local.setHeadOverlay(8, character_data.lipstick == null ? 255 : character_data.lipstick, 1, character_data.lipstickColor, character_data.lipstickColor);
		  }
		  break;
		}
	  // Skin color
	  case 17:
		{
		  character_data.skinFirst = data.config1;
		  character_data.skinSecond = data.config2;
		  character_data.skinMix = mix_data[data.config3];
		  mp.players.local.setHeadBlendData(character_data.faceFirst, character_data.faceSecond, 0, character_data.skinFirst, character_data.skinSecond, 0, character_data.faceMix, character_data.skinMix, 0, false);
		  break;
		}
	  // Eyebrow
	  case 18:
		{
		  character_data.eyebrows = data.config1;
		  character_data.eyebrowsColor1 = data.config2;
		  character_data.eyebrowsColor2 = data.config2;
		  mp.players.local.setHeadOverlayColor(2, 1, character_data.eyebrowsColor1, character_data.eyebrowsColor2);
		  break;
		}
	}
  });

mp.events.add('SeleccionarRopaPj', (data) => {

	data = JSON.parse(data);

	//mp.gui.chat.push("Slot Last: "+slot_last+" Slot Data"+ data.slot);
	if (slot_last == data.slot){
		precio = precio - precio_last;		
	}
	slot_last = data.slot;

	if (data.torso != undefined) {
		character_data.torso = data.torso;
		mp.events.callRemote('SetPlayerClothes', 3, data.torso, 0);
		//mp.gui.chat.push("Torso: "+data.torso+" Variation"+ data.variation);
	}

	if (data.undershirt != undefined) {
		character_data.undershirt = data.undershirt;
		mp.events.callRemote('SetPlayerClothes', 8, data.undershirt, 0);
		//mp.gui.chat.push("Undershirt: "+data.undershirt+" Variation"+ data.variation);
	}

	if (data.slot == 4)
		character_data.legs = data.variation;
	else if (data.slot == 6)
		character_data.feet = data.variation;
	else if (data.slot == 7)
		character_data.accessory = data.variation;
	else if (data.slot == 8){
		character_data.undershirt = data.variation;
		slot_last = 11;
	}
	else if (data.slot == 11) {
		character_data.topshirt = data.variation;
		character_data.topshirtTexture = data.texture;
	}

	precio = precio + data.precio;
	precio_last = data.precio;
	

	browserRopa.execute(`actPrecio(${precio});`);

	mp.events.callRemote('SetPlayerClothes', data.slot, data.variation, data.texture);

});