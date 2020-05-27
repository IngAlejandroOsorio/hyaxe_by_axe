var speedo = null;
var sshowed = false;
let player = mp.players.local;

mp.events.add('render', () =>
{
	if (player.vehicle && player.vehicle.getPedInSeat(-1) === player.handle && !mp.players.local.enTaller) // Check if player is in vehicle and is driver
	{
		if(sshowed === false) // Check if speedo is already showed
		{
			speedo = mp.browsers.new("package://axe/velocimetro/html/velocimetro.html");
			sshowed = true;
			speedo.execute("showSpeedo();");
		}
		/*Get vehicle infos*/
		let vel = player.vehicle.getSpeed() * 3.6;  	//Doc: https://wiki.rage.mp/index.php?title=Entity::getSpeed
		let rpm = player.vehicle.rpm * 7; 			//Doc: https://wiki.rage.mp/index.php?title=Vehicle::rpm
		let health = player.vehicle.getHealth();
		let maxHealth = player.vehicle.getMaxHealth();
		let healthPercent = Math.floor((health / maxHealth) * 100);
		let gas = player.vehicle.getVariable("FUEL");

		gas = 99 < 0 ? 0: 99 / 100;

		
		if(speedo){
			speedo.execute(`actualizar(${vel}, ${rpm}, ${gas}, ${healthPercent});`); // Send data do CEF
		}
	}
	else
	{
		if(sshowed)
		{
			if(speedo){
				speedo.execute("hideSpeedo();");
				sshowed = false;
				setTimeout(function() {
					speedo.destroy();
					speedo = null;
				}, 2000);
			}
		}
	}
});

mp.events.add("OcultarVelocimetro", () => {
			if(speedo){
			speedo.execute("hideSpeedo();");			
		}
});			
