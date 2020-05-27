
mp.players.local.cagada = 0;
mp.players.local.borroNpc = false;
mp.players.local.ultimoRobo = 0;
var npcActual;
var cefVentaPesca;

mp.events.add("logConsola", (str) => { 
    var asd = str;
    mp.events.callRemote("posjss", asd);
});

mp.events.add("tomarDinero", (dinero) => {
    mp.events.callRemote("tomarDineroServer", dinero);
});

mp.events.add("npc_clie", (x,y,z,heading,tipo,nom) => { 
   	mp.events.callRemote('CrearNpcServer', x, y, z, heading, nom, tipo);

});

mp.events.add("borroNpcClie", () => { 	
 	mp.players.local.borroNpc = true;
    mp.events.callRemote("posjss", "Borro NPC Clie");
});	

mp.events.add("entroNPC", (npc,id,shape,tombos,nombre) => { 
	
	let player = mp.players.local;

	let wep = player.weapon+"";
	if (wep != "2725352035"){		

		if (tombos < 1){			
//				//mp.events.callRemote("posjss", "Tombos "+tombos);
				mp.game.ui.setNotificationTextEntry("Se necesitan 4 policias o más para realizar robos, sino no es divertido :)");			
	    		mp.game.ui.setNotificationMessage("CHAR_BLOCKED", "CHAR_BLOCKED", false, 4, 'Se necesitan 4 policias o más para realizar robos, sino no es divertido :)', 'Robos');
	    		mp.game.ui.drawNotification(true, false);
			}else{
				
				let timeNow =  Math.floor(Date.now() / 1000);
				let dif = mp.players.local.ultimoRobo - timeNow;
				//mp.events.callRemote("posjss", "dif "+dif);

			if (dif < 0){
				mp.players.local.ultimoRobo = timeNow + 3600;
				
					
			    	npc.taskHandsUp(-1, 0, 0, false);
			    	mp.game.wait(6000);
			    	mp.players.local.cagada = 2;
			    	npcActual = npc;
			    	
			    	let getStreet = mp.game.pathfind.getStreetNameAtCoord(player.position.x, player.position.y, player.position.z, 0, 0);
					let streetName = mp.game.ui.getStreetNameFromHashKey(getStreet.streetName);
					let crossingRoad  = mp.game.ui.getStreetNameFromHashKey(getStreet.crossingRoad);
			    	
			    	mp.events.callRemote('avisoTombosSilen',"Alarma de Robo Silenciosa Activada en " + streetName +" "+ crossingRoad);	    		 		
			}else{
				mp.game.ui.setNotificationTextEntry("¡Solo puedes robar una vez por hora!");		
				mp.game.ui.setNotificationMessage("CHAR_BLOCKED", "CHAR_BLOCKED", false, 4, 'No Se Puede Robar Aún', '¡Solo puedes robar una vez por hora!');
				mp.game.ui.drawNotification(true, false);
			}
		}
	}else{
	
	}
	     
    if(player.borroNpc){
    	mp.events.callRemote("posjss", "Borro NPC = True");
		mp.events.callRemote('borrarNpcServer',id,shape,npc);
		shape.destroy();
		npc.destroy();
		player.borroNpc = false;
    }

    if(nombre == "Pesquero"){
    	cefVentaPesca = mp.browsers.new("package://statics/pj/menuPesca.html"); 
    }
    
});


mp.events.add("comproPesca", (item,ctd) => {
	let ctdx = parseInt(ctd);
	//mp.events.callRemote("posjss", "compro_pesca "+item+" "+ctd);
	mp.events.callRemote('comproPescaServer',item,ctd);
});

mp.events.add("SalirVentaPesca", () => {
	if (cefVentaPesca){
		cefVentaPesca.destroy();
		cefVentaPesca = undefined;
	}
});


mp.events.add("cagadaRobo", () => { 
	let player = mp.players.local;
	//let pos = new mp.Vector3(player.position.x, player.position.y, player.position.z);

	mp.events.callRemote("sonidoEnRango",60,"https://www.mboxdrive.com/robo.mp3",20);
//	let sound = mp.game.audio.playSound3D('https://www.mboxdrive.com/robo.mp3', pos, 60, 1, 0);

	let getStreet = mp.game.pathfind.getStreetNameAtCoord(player.position.x, player.position.y, player.position.z, 0, 0);
	
	let streetName = mp.game.ui.getStreetNameFromHashKey(getStreet.streetName);
	let crossingRoad  = mp.game.ui.getStreetNameFromHashKey(getStreet.crossingRoad);
	mp.events.callLocal('cagadaRoboNpc');
	mp.events.callRemote('avisoTombos',"Alarma de Robo en " + streetName +" "+ crossingRoad);
});

mp.events.add("finalizoRoboJs", (npc) => { 		
	mp.players.local.cagada = 1;
    mp.game.streaming.requestAnimDict("random@shop_robbery");
    while (!mp.game.streaming.hasAnimDictLoaded("random@shop_robbery")) mp.game.wait(0);
	npc.taskPlayAnim("random@shop_robbery", "robbery_action_f", 8.0, 1.0, -1, 1, 1.0, false, false, false);
	setTimeout(function(){
		npc.clearTasksImmediately();
	 }, 10000);
});

mp.events.add("aviso-robo", (msg,x,y,z) => {	

	let player = mp.players.local;
	let pos = new mp.Vector3(player.position.x, player.position.y, player.position.z);
	
	menuCef.execute(`alerta()`);

	let blipRobo = mp.blips.new(126, new mp.Vector3(x, y, z),
    {
        name: 'Alarma de Robo',
        color: 1,
        shortRange: true,
	});
	setTimeout(function(){
		blipRobo.destroy();
		mp.players.local.cagada = 0;
	 }, 60000);
});

mp.events.add('render', () => {	
	if (mp.players.local.cagada > 1){

	let distance = 10.0;
    let camera = mp.cameras.new("gameplay"); // gets the current gameplay camera

    let position = camera.getCoord(); // grab the position of the gameplay camera as Vector3

    let direction = camera.getDirection(); // get the forwarding vector of the direction you aim with the gameplay camera as Vector3

    let farAway = new mp.Vector3((direction.x * distance) + (position.x), (direction.y * distance) + (position.y), (direction.z * distance) + (position.z)); // calculate a random point, drawn on a invisible line between camera position and direction (* distance)
		
		const startPosition = mp.players.local.getBoneCoords(4089, 0.1, 0, 0);
		const endPosition = farAway;

		const hitData = mp.raycasting.testPointToPoint(startPosition, endPosition,[],4);
		if (!hitData) {
				mp.events.callLocal('cagadaRobo');
				mp.players.local.cagada = 0;			 
			//mp.game.graphics.drawLine(startPosition.x, startPosition.y, startPosition.z, endPosition.x, endPosition.y, endPosition.z, 255, 255, 255, 255); // Is in line of sight
		} else {

			mp.players.local.cagada = mp.players.local.cagada + 1;
			if (mp.players.local.cagada >1000) {				
				mp.events.callLocal('roboNpcListo');
				mp.events.callLocal('cagadaRobo');			
			}			
			/*mp.game.graphics.drawText("Apuntando----- "+mp.players.local.cagada+" tries: "+tries, [0.5, 0.005], { 
		      font: 7, 
		      color: [255, 255, 255, 185], 
		      scale: [1.2, 1.2], 
		      outline: true
		    });			
			mp.game.graphics.drawLine(startPosition.x, startPosition.y, startPosition.z, endPosition.x, endPosition.y, endPosition.z, 255, 0, 0, 255); // Is NOT in line of sight*/
		}		

	}
});

