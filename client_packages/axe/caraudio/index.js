var carAudioMenu;
var camCarMods;
var posAnt;
var headAnt;
var dimAnt;
var pdimAnt;
mp.players.local.enTaller = false;
mp.players.local.vehTaller;

var tuneoDefault = "[-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,0,0,-1,false,false,false,false,0,0,0,-1,-1]";
var modsDefaultParsed = JSON.parse(tuneoDefault);
mp.players.local.tallerMods = modsDefaultParsed;
mp.players.local.bkpMods = modsDefaultParsed;


mp.events.add("cargarCarMod", (veh,arr) => { // <------------------------------------------------------------------
    veh.carMods = JSON.parse(arr);
    mp.players.local.vehTaller = veh;
    mp.events.callRemote("aplicarModsServ", veh, veh.carMods, veh.handle );
    //mp.events.callRemote("posjss", "Cargo Mods ---- "+veh.carMods[2]+" arr: "+arr);  //+" posveh: "+veh.position.x
});



mp.events.add("aplicarMods", (veh,mods,handle) => { // mp.players.call('aplicarMods', [veh, carMods, handle]);
	//mp.events.callRemote("posjss", "recien recibidos mods: "+ mods);
	//mods = JSON.parse(mods);
	mods = JSON.parse(mods);
	//mp.events.callRemote("posjss", "Aplic Mods: "+ veh.position.x + " Lenght:" +mods.length+" mods[75]:" +mods[75]+typeof mods[75]+" handel: "+handle);
										//Aplic Mods: -628.0292358398438     Lenght:  260           mods[2]:         1string             handel: 0
	if (veh){		
	}else{
		//mp.events.callRemote("posjss", "Error cargando Mods de Vehiculo - veh no encontrado: "+ handle);
		veh = mp.vehicles.atHandle(handle);
	}
	if (!veh){
		veh = mp.players.local.vehTaller;
	}
	var i;

	if (mods.length > 1){
		for (i = 0; i < mods.length; i++) {
			if (i < 68){
				veh.setModKit(0);
				veh.setMod(i, mods[i]);
			}
		}
		mp.events.callRemote('CmenuColor', mods[68], mods[69], mods[70], veh);
		mp.events.callRemote('CmenuColor2', mods[71], mods[72], mods[73], veh);
		mp.events.callRemote('CarAudioPerlaCol', mods[74], veh);
		veh.setNeonLightEnabled(0, mods[75]); // 75 - 78 [false,false,false,false]
		veh.setNeonLightEnabled(1, mods[76]); // 75 - 78 [false,false,false,false]
		veh.setNeonLightEnabled(2, mods[77]); // 75 - 78 [false,false,false,false]
		veh.setNeonLightEnabled(3, mods[78]); // 75 - 78 [false,false,false,false]	
		veh.setNeonLightsColour(mods[79], mods[80], mods[81]);	
		veh.setWheelType(mods[82]);
		veh.setWindowTint(mods[83]);
		veh.nosAmount = mods[40];
	}else{
		mp.events.callRemote("posjss", "Error cargando Mods de Vehiculo - tipo mods: "+ typeof mods);
	}
	
	
	
});


mp.events.add('abrirTaller', () => {
//mp.keys.bind(Keys.VK_O, false, () => {
	if (mp.players.local.enTaller) return;
	mp.players.local.enTaller = true;
	var vehicle = mp.players.local.vehicle;
	mp.players.local.vehTaller = vehicle;
	var player = mp.players.local;
	if (!vehicle) return;

	if(vehicle.carMods){
		mp.players.local.tallerMods = JSON.parse(vehicle.carMods);
		mp.players.local.bkpMods = JSON.parse(vehicle.carMods);
		//mp.events.callRemote("posjss", "Abro Taller miro arr tipo: "+vehicle.carMods);
	}
		
	mp.game.cam.doScreenFadeOut(500);
	let posy = new mp.Vector3(-344.7082824707031,-137.17515563964844,38.31637954711914);		
	setTimeout(() => {mp.game.streaming.setFocusArea(posy.x, posy.y, posy.z, 0.0, 0.0, 0.0)}, 500);
    setTimeout(() => {
    	mp.events.callRemote("tpaTallerServer",mp.players.local.vehTaller); 							    	
		mp.game.cam.doScreenFadeIn(2000);
		mp.game.invoke("0x31B73D1EA9F01DA2");
    	camCarMods = mp.cameras.new("Cam", {x: -340.07, y: -135.14, z: 39}, {x: 0, y: 0, z: 121}, 50);
		camCarMods.setActive(true);
		mp.game.cam.renderScriptCams(true, true, 2000, false, false);
		vehicle.setRotation(0.0, 0.0, -96.19999694824219, 2, true);
		carAudioMenu = mp.browsers.new('package://axe/caraudio/menu.html');
		var nombre = mp.game.vehicle.getDisplayNameFromVehicleModel(vehicle.model);
		setTimeout(function(){  mp.gui.cursor.visible = true; mp.gui.cursor.show(true, true); carAudioMenu.execute(`titulo('${nombre}')`); }, 500);	
    }, 2500);
    mp.game.ui.displayRadar(false);
    mp.gui.chat.show(false);      
    player.freezePosition(true);		

});


mp.events.add('TallerCamara', (valor) => {
	valor = parseInt(valor);
	if(camCarMods){camCarMods.destroy()}
	switch (valor){
		case 0:
		camCarMods = mp.cameras.new("Cam", {x: -340.07, y: -135.14, z: 39}, {x: 0, y: 0, z: 121}, 50);
		camCarMods.setActive(true);
		mp.game.cam.renderScriptCams(true, true, 2000, false, false);
		break;
		case 1:
	    camCarMods = mp.cameras.new("Cam", {x: -341.156, y: -140.48, z: 39.028}, {x: 0, y: 0, z: 30.910}, 50);
		camCarMods.setActive(true);
		mp.game.cam.renderScriptCams(true, true, 20000000000000000000000000, false, false);
		break;
		case 2:
		camCarMods = mp.cameras.new("Cam", {x: -339.07, y: -137.44, z: 40}, {x: -20, y: 0, z: 84.7}, 50);
		camCarMods.setActive(true);
		mp.game.cam.renderScriptCams(true, true, 20000000000000000000000000, false, false);
		break;
	}
});

mp.events.add('TallerCamaraRot', (rot) => {
  rot = parseFloat(rot);
  //if (camCarMods)  {camCarMods.setRot(0.0, 0.0, rot, 2);} 
  //mp.events.callRemote("posjss", "rot coche-> "+rot);
  mp.players.local.vehTaller.setRotation(0.0, 0.0, rot, 2, true);
});

mp.events.add('RepararTaller', () => {
	mp.events.callRemote("RepararTallerServ");
});




mp.events.add('CobrarTaller', (precio) => {
	precio = parseInt(precio);
	mp.events.callRemote("CobrarTallerServ",precio);
});

mp.events.add('guardarTaller', () => {
	if (!mp.players.local.enTaller) return;
	mp.events.callRemote("guardarModsServ", JSON.stringify(mp.players.local.tallerMods), mp.players.local.vehTaller);
	//guardo array
	if (carAudioMenu){
		mp.game.cam.doScreenFadeOut(50);		
		carAudioMenu.destroy();
		setTimeout(function(){  mp.gui.cursor.visible = false; mp.gui.cursor.show(false, false); mp.game.cam.doScreenFadeIn(2000)}, 2000);
		if(camCarMods){camCarMods.setActive(false);camCarMods.destroy()}
		mp.game.cam.renderScriptCams(false, true, 2000, false, false);
		mp.events.callRemote("tpdeTallerServer",mp.players.local.vehTaller);
		mp.game.ui.displayRadar(true);
    	mp.gui.chat.show(true);      
    	mp.players.local.freezePosition(false);
    	setTimeout(() => { mp.players.local.enTaller = false}, 10000);
	}	
});

mp.events.add('cerrarTunning', () => {
	if (!mp.players.local.enTaller) return;	
	if (carAudioMenu){
		mp.game.cam.doScreenFadeOut(50);		
		carAudioMenu.destroy();
		setTimeout(function(){  mp.gui.cursor.visible = false; mp.gui.cursor.show(false, false); mp.game.cam.doScreenFadeIn(2000)}, 2000);
		if(camCarMods){camCarMods.setActive(false);camCarMods.destroy()}
		mp.game.cam.renderScriptCams(false, true, 2000, false, false);
		mp.events.callRemote("tpdeTallerServer",mp.players.local.vehTaller);
		mp.game.ui.displayRadar(true);
    	mp.gui.chat.show(true);      
    	mp.players.local.freezePosition(false);
    	
    	mp.players.local.tallerMods = modsDefaultParsed;
    	mp.events.call("aplicarMods", mp.players.local.vehTaller, JSON.stringify(mp.players.local.bkpMods),mp.players.local.vehTaller.handle);
    	setTimeout(() => {mp.players.local.enTaller = false;}, 10000);
	}	
});


mp.events.add('mejoraCoche', (idx,val) => {
	mp.players.local.tallerMods[parseInt(idx)] = parseInt(val);
	var vehicle = mp.players.local.vehTaller;
	vehicle.setModKit(0);
	setTimeout(function(){ vehicle.setMod(idx, val) }, 100);	// 0 - 67
});


mp.events.add("ColorCocheToServer", (red, green, blue) =>{ // 68 - 70
	var veh = mp.players.local.vehTaller;
	mp.players.local.tallerMods[68] = parseInt(red);
	mp.players.local.tallerMods[69] = parseInt(green);
	mp.players.local.tallerMods[70] = parseInt(blue);
	mp.events.callRemote('CmenuColor', red, green, blue, veh);
});

mp.events.add("ColorCocheToServerTwo", (red, green, blue) =>{ // 71 - 73
	var veh = mp.players.local.vehTaller;
	mp.players.local.tallerMods[71] = parseInt(red);
	mp.players.local.tallerMods[72] = parseInt(green);
	mp.players.local.tallerMods[73] = parseInt(blue);
	mp.events.callRemote('CmenuColor2', red, green, blue, veh);
});

mp.events.add("CochePerlado", (inte) =>{ // 74
	var veh = mp.players.local.vehTaller;
	mp.players.local.tallerMods[74] = parseInt(inte);
	mp.events.callRemote('CarAudioPerlaCol', inte, veh);
});

mp.events.add("NeonToggle", (index,estado) =>{	// 75 - 78 [false,false,false,false] 	
	var idx = 75;
	switch (index){
		case 1:
			idx = 76
		break;
		case 2:
			idx = 77
		break;
		case 3:
			idx = 78
		break;
	}
	mp.players.local.tallerMods[idx] = estado;
	var vehicle = mp.players.local.vehTaller;
	vehicle.setNeonLightEnabled(index, estado);
});

mp.events.add("ColorCocheNeon", (red, green, blue) =>{ //79 - 81
	mp.players.local.tallerMods[79] = parseInt(red);
	mp.players.local.tallerMods[80] = parseInt(green);
	mp.players.local.tallerMods[81] = parseInt(blue);
	var vehicle = mp.players.local.vehTaller;
	vehicle.setNeonLightsColour(parseInt(red), parseInt(green), parseInt(blue));
});

mp.events.add("TipoLlanta", (inte) =>{ // 82
	mp.players.local.tallerMods[82] = parseInt(inte);
	var vehicle = mp.players.local.vehTaller;
	vehicle.setWheelType(parseInt(inte));
});

mp.events.add("TipoVentana", (inte) =>{ //83
	mp.players.local.tallerMods[83] = parseInt(inte);
	var vehicle = mp.players.local.vehTaller;
	vehicle.setWindowTint(parseInt(inte));
});



mp.keys.bind(Keys.VK_BACK, false, () => {
	if (!mp.players.local.enTaller) return;
	if (carAudioMenu){
		mp.game.cam.doScreenFadeOut(50);		
		carAudioMenu.destroy();
		setTimeout(function(){  mp.gui.cursor.visible = false; mp.gui.cursor.show(false, false); mp.game.cam.doScreenFadeIn(2000)}, 2500);
		if(camCarMods){camCarMods.setActive(false);camCarMods.destroy()}
		mp.game.cam.renderScriptCams(false, true, 2000, false, false);
		mp.events.callRemote("tpdeTallerServer",mp.players.local.vehTaller);
		mp.game.ui.displayRadar(true);
    	mp.gui.chat.show(true);      
    	mp.players.local.freezePosition(false);
    	
    	mp.players.local.tallerMods = modsDefaultParsed;
    	mp.events.call("aplicarMods", mp.players.local.vehTaller, JSON.stringify(mp.players.local.bkpMods),mp.players.local.vehTaller.handle);
    	setTimeout(() => {mp.players.local.enTaller = false}, 10000);
	}
});

mp.keys.bind(Keys.VK_ESCAPE, false, () => {
	if (!mp.players.local.enTaller) return;
	if (carAudioMenu){
		mp.game.cam.doScreenFadeOut(50);
		carAudioMenu.destroy();
		setTimeout(function(){  mp.gui.cursor.visible = false; mp.gui.cursor.show(false, false); mp.game.cam.doScreenFadeIn(2000)}, 2000);
		if(camCarMods){camCarMods.setActive(false);camCarMods.destroy()}
		mp.game.cam.renderScriptCams(false, true, 2000, false, false);
		mp.events.callRemote("tpdeTallerServer",mp.players.local.vehTaller);
		mp.game.ui.displayRadar(true);
    	mp.gui.chat.show(true);      
    	mp.players.local.freezePosition(false);
    	
    	mp.players.local.tallerMods = modsDefaultParsed;
    	mp.events.call("aplicarMods", mp.players.local.vehTaller, JSON.stringify(mp.players.local.bkpMods),mp.players.local.vehTaller.handle);
    	setTimeout(() => {mp.players.local.enTaller = false}, 10000);
	}
});




const localPlayer = mp.players.local;
let activateNitro = false;
let vehiclesWithNitro = [];
let exhausts = ["exhaust", "exhaust_2", "exhaust_4", "exhaust_5", "exhaust_6", "exhaust_7"];

mp.game.streaming.requestNamedPtfxAsset("core");

mp.events.add({
    'toggleNitroEffect': (state, v) => {
        if (state) {
            if (v && v.handle !== 0) vehiclesWithNitro.push(v);
        } else {
            let indx = vehiclesWithNitro.indexOf(v);
            if (indx != -1) {
                vehiclesWithNitro.splice(indx, 1);
            }
        }
    },

    'giveNitro': (amount, infinite) => {
        mp.game.invoke("0xF2CC03703BDF51A4", localPlayer.vehicle.handle, amount, infinite ? 1 : 0);
    }
});

mp.events.add('render', () => {
    if (mp.game.controls.isControlPressed(0, 73) && localPlayer.vehicle) {
        if (!activateNitro && localPlayer.vehicle.nosAmount > -1.1) {
                if (localPlayer.vehicle.nosAmount != -1){
                toggleNitro(true);
                let tiempo = localPlayer.vehicle.nosAmount + 1;
                localPlayer.vehicle.setForwardSpeed(localPlayer.vehicle.getSpeed() + 20);
                setTimeout(() => {
                localPlayer.vehicle.setForwardSpeed(localPlayer.vehicle.getSpeed() - 15);
                localPlayer.vehicle.nosAmount = -1;      
             }, tiempo * 1000);
            }            
        }
    }

    if (localPlayer.vehicle && localPlayer.vehicle.nosAmount < -0.9) {
        toggleNitro(false);
    }
    
    if (vehiclesWithNitro.length > 0) {
        vehiclesWithNitro.forEach((v) => {
            try {
                if (mp.game.streaming.hasNamedPtfxAssetLoaded("core")) {
                    let heading = v.getHeading();
                    let pitch = v.getPitch();
                    exhausts.forEach((element) => {
                        let boneIndex = mp.game.invoke('0xFB71170B7E76ACBA', v.handle, element); // GET_ENTITY_BONE_INDEX_BY_NAME
                        if (boneIndex >= 0) {
                            let boneCoords = v.getWorldPositionOfBone(boneIndex);
                            mp.game.graphics.setPtfxAssetNextCall("core");
                            if (mp.game.controls.isControlPressed(0, 71)) {
                                mp.game.graphics.startParticleFxNonLoopedAtCoord("veh_backfire", boneCoords.x, boneCoords.y, boneCoords.z,
                                0.0, pitch, heading - 88, 1, false, false, false);
                            } else {
                                mp.game.graphics.startParticleFxNonLoopedAtCoord("veh_backfire", boneCoords.x, boneCoords.y, boneCoords.z,
                                0.0, pitch, heading - 88, 0.4, false, false, false);
                            }
                           
                        }
                    });
                } else {
                    mp.game.streaming.requestNamedPtfxAsset("core");
                }
            } catch (e) {
                mp.gui.chat.push(e.toString());
            }
        });
    }
});

function toggleNitro(state) {
    if (state) {
        activateNitro = true;
        mp.events.callRemote("NITRO_START");
    } else {
        activateNitro = false;
        mp.events.callRemote("NITRO_STOP");
    }
};

