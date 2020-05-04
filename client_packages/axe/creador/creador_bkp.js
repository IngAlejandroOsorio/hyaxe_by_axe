let browser;
let camera;

var arrayCara = [0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0];
var character_data2 = {
  name: "",
  gender: 0,
  faceFirst: 0,
  faceSecond: 0,
  faceMix: 0.0,
  skinFirst: 0,
  skinSecond: 0,
  skinMix: 0.0,  
  hairType: 0,
  hairColor: 0,
  hairHighlight: 0,
  eyeColor: 0,
  eyebrows: 0,
  eyebrowsColor1: 0,
  eyebrowsColor2: 0,
  beard: null,
  beardColor: 0,
  makeup: null,
  makeupColor: 0,
  lipstick: null,
  lipstickColor: 0,
  torso: 0,
  legs: 1,
  feet: 1,
  undershirt: 57,
  topshirt: 1,
  topshirtTexture: 0,
  accessory: 0,
};

mp.events.add("ShowCharacterCreator", () => {
  if (!browser) {
    browser = mp.browsers.new("package://statics/pj/creator.html");    
  }
    var camPos = new mp.Vector3(403.6378, -998.5422, -99.00404);
    var camRot = new mp.Vector3(0.0, 0.0, 176.891);

    camera = mp.cameras.new('lookAtBody', camPos, camRot, 40);
    camera.pointAtCoord(402.9198, -996.5348, -99.00024);
    camera.setActive(true);

    mp.game.cam.renderScriptCams(true, false, 2000, true, false);
  resetCharacterCreation();
});

function resetCharacterCreation() {

character_data2.torso = character_data2.gender ? 5 : 0;
  character_data2.undershirt = character_data2.gender ? 95 : 57;
  character_data2.hairType = character_data2.gender ? 4 : 0;

  mp.events.callRemote('SetPlayerClothes', 2, character_data2.hairType, 0);
  mp.events.callRemote('SetPlayerClothes', 3, character_data2.torso, 0);
  mp.events.callRemote('SetPlayerClothes', 4, character_data2.legs, 0);
  mp.events.callRemote('SetPlayerClothes', 6, character_data2.feet, 0);
  mp.events.callRemote('SetPlayerClothes', 7, character_data2.accessory, 0);
  mp.events.callRemote('SetPlayerClothes', 8, character_data2.undershirt, 0);
  mp.events.callRemote('SetPlayerClothes', 11, character_data2.topshirt, 0);
  mp.events.callRemote("SetPlayerSkin", character_data2.gender ? 'FreemodeFemale01' : 'FreeModeMale01');

  mp.players.local.setHeadBlendData(character_data2.faceFirst, character_data2.faceSecond, 0, character_data2.skinFirst, character_data2.skinSecond, 0, character_data2.faceMix, character_data2.skinMix, 0, false);
  mp.players.local.setHairColor(character_data2.hairColor, character_data2.hairHighlight);
  mp.players.local.setEyeColor(0);

  mp.players.local.setHeadOverlayColor(2, 1, character_data2.eyebrowsColor1, character_data2.eyebrowsColor2);
  mp.players.local.setHeadOverlayColor(4, 0, character_data2.makeupColor, character_data2.makeupColor);
  mp.players.local.setHeadOverlayColor(8, 2, character_data2.lipstickColor, character_data2.lipstickColor);

}

mp.events.add("ChangeCharacterGender", (id) => {
  character_data2.gender = id;
  character_data2.faceFirst = 0;
  character_data2.faceSecond = 0;
  character_data2.faceMix = 0.0;
  character_data2.skinFirst = 0;
  character_data2.skinSecond = 0;
  character_data2.skinMix = 0.0;
  character_data2.hairType = 0;
  character_data2.hairColor = 0;
  character_data2.hairHighlight = 0;
  character_data2.eyeColor = 0;
  character_data2.eyebrows = 0;
  character_data2.eyebrowsColor1 = 0;
  character_data2.eyebrowsColor2 = 0;
  character_data2.beard = null;
  character_data2.beardColor = 0;
  character_data2.makeup = null;
  character_data2.makeupColor = 0;
  character_data2.lipstick = null;
  character_data2.lipstickColor = 0;
  character_data2.torso = 0;
  character_data2.legs = 1;
  character_data2.feet = 1;
  character_data2.undershirt = 57;
  character_data2.topshirt = 1;
  character_data2.topshirtTexture = 0;
  character_data2.accessory = 0;
  
  mp.events.callRemote("SetPlayerSkin", id);
  arrayCara = [0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0];
  resetCharacterCreation();

});

mp.events.add('MoveCameraPosition', (pos) => {
  //browser.execute(`logeo('RAGEEEEEEEEEEE: Camara ------'+"${pos}")`);
  if (camera)  {camera.destroy()}
  switch (pos) {
    case 0:
      {
        // Head
        var camPos = new mp.Vector3(402.9378, -997.0, -98.35);
        var camRot = new mp.Vector3(0.0, 0.0, 176.891);

        camera = mp.cameras.new('lookAtHead', camPos, camRot, 40);
        camera.pointAtCoord(402.9198, -996.5348, -98.35);
        camera.setActive(true);

        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
    case 1:
      {
        // Torso
        var camPos = new mp.Vector3(402.9378, -997.5, -98.60);
        var camRot = new mp.Vector3(0.0, 0.0, 176.891);

        camera = mp.cameras.new('lookAtTorso', camPos, camRot, 40);
        camera.pointAtCoord(402.9198, -996.5348, -98.60);
        camera.setActive(true);

        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
    case 2:
      {
        // Legs
        var camPos = new mp.Vector3(402.9378, -997.5, -99.40);
        var camRot = new mp.Vector3(0.0, 0.0, 176.891);

        camera = mp.cameras.new('lookAtLegs', camPos, camRot, 40);
        camera.pointAtCoord(402.9198, -996.5348, -99.40);
        camera.setActive(true);

        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
    case 3:
      {
        // Feet
        var camPos = new mp.Vector3(402.9378, -997.5, -99.85);
        var camRot = new mp.Vector3(0.0, 0.0, 176.891);

        camera = mp.cameras.new('lookAtFeet', camPos, camRot, 40);
        camera.pointAtCoord(402.9198, -996.5348, -99.85);
        camera.setActive(true);

        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
    default:
      {
        // Default
        var camPos = new mp.Vector3(403.6378, -998.5422, -99.00404);
        var camRot = new mp.Vector3(0.0, 0.0, 176.891);

        camera = mp.cameras.new('lookAtBody', camPos, camRot, 40);
        camera.pointAtCoord(402.9198, -996.5348, -99.00024);
        camera.setActive(true);

        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
  }
});

mp.events.add('AccionCamara', (data) => {
  data = JSON.parse(data);
  //browser.execute(`logeo('RAGEEEEEEEEEEE: AccionCamara ------'+"${data.xx}---${data.yy}")`);
  if (camera)  {camera.setCoord(403.6378, data.xx, data.yy)}
});

mp.events.add('AccionCamaraRot', (data) => {
  data = JSON.parse(data);
  //browser.execute(`logeo('RAGEEEEEEEEEEE: AccionCamara ------'+"${data.zz}")`);
  if (camera)  {camera.setRot(0.0, 0.0, data.zz, 2);}
  mp.players.local.setRotation(0.0, 0.0, data.zz, 2, true);
});

mp.events.add('SelectCharacterClothes', (data) => {
  data = JSON.parse(data);
  if (data.torso != undefined) {
    character_data2.torso = data.torso;
    mp.events.callRemote('SetPlayerClothes', 3, data.torso, 0);
  }
  if (data.undershirt != undefined) {
    character_data2.undershirt = data.undershirt;
    mp.events.callRemote('SetPlayerClothes', 8, data.undershirt, 0);
  }
  if (data.slot == 4)
    character_data2.legs = data.variation;
  else if (data.slot == 6)
    character_data2.feet = data.variation;
  else if (data.slot == 7)
    character_data2.accessory = data.variation;
  else if (data.slot == 8)
    character_data2.undershirt = data.variation;
  else if (data.slot == 11) {
    character_data2.topshirt = data.variation;
    character_data2.topshirtTexture = data.texture;
  }
  mp.events.callRemote('SetPlayerClothes', data.slot, data.variation, data.texture);
});


mp.events.add("GoBackToSelection", () => {
  if (browser) {
    browser.destroy();
    browser = undefined;
  }

  mp.events.callRemote("RetrieveCharactersList");
});

mp.events.add('SelComponenteCara', (data) => {
  data = JSON.parse(data);
  arrayCara[data.index] = data.valor;
  mp.players.local.setFaceFeature(data.index, data.valor);
});

mp.events.add('SelMaquillaje', (data) => {
  data = JSON.parse(data);
  character_data2.makeup = data.tipo;
  character_data2.makeupColor = data.color;
  mp.players.local.setHeadOverlay(4, data.tipo, 1.0, data.color, data.color);
});


mp.events.add('SelComponenteRasgos', (data) => {
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
        character_data2.faceFirst = data.config1;
        character_data2.faceSecond = data.config2;
        character_data2.faceMix = mix_data[data.config3];
        mp.players.local.setHeadBlendData(character_data2.faceFirst, character_data2.faceSecond, 0, character_data2.skinFirst, character_data2.skinSecond, 0, character_data2.faceMix, character_data2.skinMix, 0, false);
        break;
      }
    // Eyes
    case 14:
      {
        character_data2.eyeColor = data.config1;
        mp.players.local.setEyeColor(character_data2.eyeColor);
        break;
      }
    // Hair
    case 15:
      {
        character_data2.hairType = data.config1;
        character_data2.hairColor = data.config2;
        character_data2.hairHighlight = data.config3;
        mp.players.local.setHairColor(character_data2.hairColor, character_data2.hairHighlight);
        mp.events.callRemote('SetPlayerClothes', 2, character_data2.hairType, 0);
        break;
      }
    case 16:
      {
        if (!character_data2.gender) {
          if (data.config1 < 1) {
            character_data2.beard = null;
          }
          else {
            character_data2.beard = data.config1 - 1;
          }
          character_data2.beardColor = data.config2;
          mp.players.local.setHeadOverlay(1, character_data2.beard == null ? 255 : character_data2.beard, 1, character_data2.beardColor, character_data2.beardColor);
        }
        else {
          if (data.config1 < 1) {
            character_data2.lipstick = null;
          }
          else {
            character_data2.lipstick = data.config1 - 1;
          }
          character_data2.lipstickColor = data.config2;
          mp.players.local.setHeadOverlay(8, character_data2.lipstick == null ? 255 : character_data2.lipstick, 1, character_data2.lipstickColor, character_data2.lipstickColor);
        }
        break;
      }
    // Skin color
    case 17:
      {
        character_data2.skinFirst = data.config1;
        character_data2.skinSecond = data.config2;
        character_data2.skinMix = mix_data[data.config3];
        mp.players.local.setHeadBlendData(character_data2.faceFirst, character_data2.faceSecond, 0, character_data2.skinFirst, character_data2.skinSecond, 0, character_data2.faceMix, character_data2.skinMix, 0, false);
        break;
      }
    // Eyebrow
    case 18:
      {
        character_data2.eyebrows = data.config1;
            character_data2.eyebrowsColor1 = data.config2;
            character_data2.eyebrowsColor2 = data.config2;
            mp.players.local.setHeadOverlay(2, character_data2.eyebrows == null ? 255 : character_data2.eyebrows, 1, character_data2.eyebrowsColor1, character_data2.eyebrowsColor2);
            break;
      }
  }
});

mp.events.add('finishCharacterCreation', (character_name) => {
  //browser.execute(`logeo(${JSON.stringify(character_data2)})`);
  if (browser) {
    browser.destroy();
    browser = undefined;
  }

  mp.gui.cursor.visible = false;
  mp.game.cam.renderScriptCams(false, false, 0, true, false);
  character_data2.name = character_name;  
  mp.events.callRemote('FinishCharacterCreation', JSON.stringify(character_data2),JSON.stringify(arrayCara));
});

