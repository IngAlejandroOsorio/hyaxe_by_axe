var menuPesca;
var objPesca;
var aPesca;
var childPesca;
mp.players.local.manoIzq = "nada";
mp.players.local.estaPescando = false;

let blipPesca = mp.blips.new(68, new mp.Vector3(-1017.2368,-1363.595,5.553193, 0),
    {
        name: 'Venta de Pescado',
        color: 30,
        shortRange: true,
});

mp.players.local.pescando = false;
mp.players.local.puedePescar = false;
mp.players.local.puedePescarMsg = " ...Aqui Puedes Pescar Algo... ";


mp.events.add("equiparPesca", () => { 
	if (mp.players.local.manoIzq == "Caña"){return}
	mp.players.local.manoIzq = "Caña";
	mp.players.local.pescando = true;
	objPesca = mp.objects.new(mp.game.joaat("prop_fishing_rod_01"), mp.players.local.position);
	var a = mp.players.local.getBoneIndex(36029);
	setTimeout(function(){
		childPesca = mp.objects.new(mp.game.joaat("ng_proc_sodacan_01b"), mp.players.local.position, {alpha: 0}); 
		objPesca.attachTo(mp.players.local.handle, a, .15, .15, .05, 270, -90, -30, !0, !1, !1, !1, 0, !0);
		childPesca.attachTo(mp.players.local.handle, a, 1.4, 2.3, .05, 270, -90, -30, !0, !1, !1, !1, 0, !0); 
		childPesca.setCollision(!1,!0);	
	}, 200);
});	

/*
mp.keys.bind(Keys.VK_O, false, () => { 	

	mp.players.local.pescando = true;
	objPesca = mp.objects.new(mp.game.joaat("prop_fishing_rod_01"), mp.players.local.position);
	var a = mp.players.local.getBoneIndex(36029);
	childPesca = mp.objects.new(mp.game.joaat("ng_proc_sodacan_01b"), mp.players.local.position, {alpha: 0}); 
	objPesca.attachTo(mp.players.local.handle, a, .15, .15, .05, 270, -90, -30, !0, !1, !1, !1, 0, !0);
	childPesca.attachTo(mp.players.local.handle, a, 1.4, 2.3, .05, 270, -90, -30, !0, !1, !1, !1, 0, !0); 
	childPesca.setCollision(!1,!0);	

});	
*/

mp.keys.bind(Keys.VK_P, false, () => { 	
if (mp.players.local.estaPescando) return;
mp.players.local.estaPescando = true;
if (mp.players.local.pescando && mp.players.local.puedePescar){

		/*var obj = mp.objects.new(mp.game.joaat("prop_fishing_rod_01"), mp.players.local.position);
		var a = mp.players.local.getBoneIndex(36029);
		var child = mp.objects.new(mp.game.joaat("ng_proc_sodacan_01b"), mp.players.local.position, {alpha: 0}); 
		obj.attachTo(mp.players.local.handle, a, .15, .15, .05, 270, -90, -30, !0, !1, !1, !1, 0, !0);
		child.attachTo(mp.players.local.handle, a, 1.4, 2.3, .05, 270, -90, -30, !0, !1, !1, !1, 0, !0); 
		child.setCollision(!1,!0);*/
		setTimeout(() => {mp.gui.cursor.visible = true;mp.gui.cursor.show(false, true)},200);

		mp.events.callRemote("sonidoEnRango",10,"https://retired.sounddogs.com/previews/2226/mp3/518039_SOUNDDOGS__fi.mp3",0);
		
	    mp.game.streaming.requestAnimDict("amb@world_human_stand_fishing@base");
	    while (!mp.game.streaming.hasAnimDictLoaded("amb@world_human_stand_fishing@base")) mp.game.wait(0);
		mp.players.local.taskPlayAnim("amb@world_human_stand_fishing@base", "base", 8.0, 1.0, -1, 1, 1.0, false, false, false);		

		let espera = Math.floor(Math.random() * 40000);
		
		mp.players.local.puedePescarMsg = " ... Pescando ... ";

		setTimeout(function(){  
			mp.events.callRemote("sonidoEnRango",10,"https://retired.sounddogs.com/previews/2226/mp3/518009_SOUNDDOGS__fi.mp3",0);
			menuPesca = mp.browsers.new("package://axe/trabajos/pesca/index.html");
		}, espera);

	}/*else{
		mp.game.ui.setNotificationTextEntry("STRING");
    	mp.game.ui.setNotificationMessage("CHAR_BLOCKED", "CHAR_BLOCKED", false, 4, 'No se Puede Pescar', 'Necesitas buscar Agua Profunda');
   		mp.game.ui.drawNotification(true, false);
	}*/	
});

mp.events.add("finalizarPesca", (pescado) => { 	      
	mp.events.callRemote("DarPescado",pescado);
	mp.events.callRemote("sonidoEnRango",10,"https://retired.sounddogs.com/previews/2226/mp3/518013_SOUNDDOGS__fi.mp3",0);
	mp.players.local.clearTasksImmediately();
	mp.players.local.puedePescarMsg = " ...Aqui Puedes Pescar Algo... ";
	mp.players.local.estaPescando = false;
	setTimeout(function(){
		if (menuPesca){
		menuPesca.destroy();
		menuPesca = undefined;		
		}		
	}, 2500);
});

mp.keys.bind(Keys.VK_BACK, false, () => {
	if (mp.players.local.pescando){
		setTimeout(() => {mp.gui.cursor.visible = false;mp.gui.cursor.show(false, false)},200);
		mp.players.local.pescando = false;		
		mp.players.local.manoIzq = "";
		mp.events.callRemote("DarPescado","Caña");
		if (objPesca && childPesca){
			childPesca.destroy();
			objPesca.destroy();
		}
		mp.players.local.clearTasksImmediately();
		if (menuPesca){
			menuPesca.destroy();
			menuPesca = undefined;	
		}
		mp.players.local.estaPescando = false;
	}	
});

mp.keys.bind(Keys.VK_ESCAPE, false, () => {
	if (mp.players.local.pescando){
		setTimeout(() => {mp.gui.cursor.visible = false;mp.gui.cursor.show(false, false)},200);
		mp.players.local.pescando = false;
		mp.players.local.manoIzq = "";
		mp.events.callRemote("DarPescado","Caña");
		if (objPesca && childPesca){
			childPesca.destroy();
			objPesca.destroy();
		}
		mp.players.local.clearTasksImmediately();
		if (menuPesca){
			menuPesca.destroy();
			menuPesca = undefined;	
		}
		mp.players.local.estaPescando = false;
	}	
});


mp.events.add('render', () => {	
	if (mp.players.local.pescando){ 

	let distance = 30.0;
    let camera = mp.cameras.new("gameplay"); 

    let position = camera.getCoord(); 

    let direction = camera.getDirection(); 

    let farAway = new mp.Vector3((direction.x * distance) + (position.x), (direction.y * distance) + (position.y), (direction.z * distance) + (position.z)); // calculate a random point, drawn on a invisible line between camera position and direction (* distance)

    var estaEnAgua = mp.game.water.getWaterHeight(farAway.x, farAway.y, farAway.z, 1);
		
		const startPosition = mp.players.local.getBoneCoords(25260, 0.1, 0, 0);
		const endPosition = farAway;

		const hitData = mp.raycasting.testPointToPoint(startPosition, endPosition,[],-1);
		if (!hitData && estaEnAgua < 1 && estaEnAgua != 0 && !mp.players.local.vehicle) {
				mp.players.local.puedePescar = true;				
				mp.game.graphics.drawText(mp.players.local.puedePescarMsg, [0.5, 0.105], { 
			      font: 7, 
			      color: [255, 255, 255, 185], 
			      scale: [1.0, 1.0], 
			      outline: true
			    });	
						
		} else {
			mp.players.local.puedePescar = false;		
		}		

	}
});