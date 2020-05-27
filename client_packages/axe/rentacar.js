
mp.players.local.rentando = false;

mp.events.add("rentingLoopJs", () => { // <------------------------------------------------------------------
	mp.players.local.rentando = true;
	var lupe = setInterval(() => { 
		if(mp.players.local.rentando){
			mp.events.callRemote("RentVehiclePin");
		}else{
			clearInterval(lupe);
		}
	}, 60000);    
    //mp.events.callRemote("posjss", "Inicia Lupe Rent");
});


mp.events.add("rentingLoopJs", () => { // <------------------------------------------------------------------
	mp.players.local.rentando = false;
	mp.events.callRemote("RentVehicleFinish");
});