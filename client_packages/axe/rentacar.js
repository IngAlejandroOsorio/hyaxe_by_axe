
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


mp.game.ped.removeScenarioBlockingArea(1,true);
mp.game.streaming.setPedPopulationBudget(3);
mp.game.ped.setCreateRandomCops(true);
mp.game.vehicle.setRandomBoats(true);
mp.game.vehicle.setRandomTrains(true);
mp.game.vehicle.setGarbageTrucks(true);
mp.game.streaming.setVehiclePopulationBudget(3);
mp.game.invoke('0x34AD89078831A4BC'); // SET_ALL_VEHICLE_GENERATORS_ACTIVE
mp.game.vehicle.setAllLowPriorityVehicleGeneratorsActive(true);
mp.game.vehicle.setNumberOfParkedVehicles(-1);
mp.game.vehicle.displayDistantVehicles(true);
mp.game.graphics.disableVehicleDistantlights(false);

const trainModels = [
    'freight', 'freightcar',
    'freightgrain', 'freightcont1',
    'freightcont2', 'freighttrailer',
    'tankercar', 'metrotrain',
    's_m_m_lsmetro_01',
];

trainModels.forEach((el) => {
    mp.game.streaming.requestModel(mp.game.joaat(el));
    mp.console.logInfo(`is loaded ${mp.game.streaming.hasModelLoaded(mp.game.joaat(el))}`);
});
setInterval(() => {
    mp.game.vehicle.createMissionTrain(24, 247.9364, -1198.597, 37.4482, true);
}, 5000);
