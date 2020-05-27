
let tallerDimension = 10;


mp.events.add({
'NITRO_START': (player) => {
    if (player.vehicle)
    mp.players.call('toggleNitroEffect', [true, player.vehicle]);
},

'NITRO_STOP': (player) => {
    if (player.vehicle)
    mp.players.call('toggleNitroEffect', [false, player.vehicle]);
}
});


mp.events.add('aplicarModsServ', (player, veh, carMods, handle ) => {
  	if (veh){
  		player.call('aplicarMods', [veh, carMods, handle]);
  	}
});

mp.events.add('tpaTallerServer', (player,veh) => {
  	var vehicle = veh;
  	if (vehicle){
	  	let posy = new mp.Vector3(-344.7082824707031,-137.17515563964844,38.31637954711914);
	  	//player.call("goposjs", ["position al ir al taller: "+vehicle.position.x]);
			
		player.tallerPosAnt = vehicle.position;
		player.tallerHeadAnt = vehicle.heading;

		player.dimension = tallerDimension;
		player.position = posy;
		vehicle.dimension = tallerDimension;
		vehicle.position = posy;
		vehicle.heading = -6.038556;
		setTimeout(function(){ player.putIntoVehicle(vehicle, 0); }, 500);

	  	tallerDimension++;
  	}
});

mp.events.add('tpdeTallerServer', (player,veh) => {
  	var vehicle = veh;

	player.dimension = 0;
	player.position = player.tallerPosAnt;
	vehicle.position = player.tallerPosAnt;
	vehicle.heading = player.tallerHeadAnt;	
	vehicle.dimension = 0;
	setTimeout(function(){ player.putIntoVehicle(vehicle, 0); }, 500);

});