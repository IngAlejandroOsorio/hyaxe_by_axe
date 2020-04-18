var speedo = null;
var sshowed = false;
let player = mp.players.local;

mp.events.add('render', () =>
{
	if (player.vehicle && player.vehicle.getPedInSeat(-1) === player.handle) // Check if player is in vehicle and is driver
	{
		if(sshowed === false) // Check if speedo is already showed
		{
			speedo = mp.browsers.new("package://velocimetro_axe/html/velocimetro.html");
			sshowed = true;
		}
		/*Get vehicle infos*/
		let vel = player.vehicle.getSpeed() * 2.8;  	//Doc: https://wiki.rage.mp/index.php?title=Entity::getSpeed
		let rpm = player.vehicle.rpm * 7; 			//Doc: https://wiki.rage.mp/index.php?title=Vehicle::rpm
		let health = player.vehicle.getHealth();
		let maxHealth = player.vehicle.getMaxHealth();
		let healthPercent = Math.floor((health / maxHealth) * 100);
		let gas = player.vehicle.getPetrolTankHealth();
		gas = gas < 0 ? 0: gas / 10;
		
		speedo.execute(`actualizar(${vel}, ${rpm}, ${gas}, ${healthPercent});`); // Send data do CEF
	}
	else
	{
		if(sshowed)
		{
			speedo.execute("hideSpeedo();");
			sshowed = false;
			setTimeout(function() {
				speedo.destroy();
				speedo = null;
			}, 2000);
		}
	}
});


