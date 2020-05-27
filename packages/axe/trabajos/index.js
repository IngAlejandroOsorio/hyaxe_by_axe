

mp.events.add("sonidoEnRango", (player,rango,url,segundos) => {           
	mp.players.callInRange(player.position, rango, 'sonido', [url,segundos]);
});

mp.events.add("crearPasajerosServ", (player,vehicle) => {
    var str = "Nuevo NPC "+nombre+" "+tipo+" creado en pos: "+vehicle.position.x+ " "+vehicle.position.y+" "+vehicle.position.z+" --- heading: "+vehicle.heading;
    player.call("logConsola", [str]);
    let staticPed = mp.peds.new(mp.joaat(tipo), vehicle.position,
    {    
          dynamic: true, // still server-side but not sync'ed anymore
          frozen: false,
          invincible: true
    },vehicle.heading);

    player.call("crearPasajerosCliente",[vehicle]);
});

mp.events.addCommand("test", (player) => {
	let vehicle = player.vehicle;
    var str = "Nuevo NPC creado en pos: "+vehicle.position.x+ " "+vehicle.position.y+" "+vehicle.position.z+" --- heading: "+vehicle.heading;
    player.call("logConsola", [str]);
    let staticPed = mp.peds.new(mp.joaat("Orleans"), new mp.Vector3(vehicle.position.x + 2.0, vehicle.position.y + 2.0, 326),
    {    
          dynamic: true, // still server-side but not sync'ed anymore
          frozen: false,
          invincible: true
    },vehicle.heading);

    player.call("crearPasajerosCliente",[staticPed,vehicle]);
});


mp.events.addCommand("test2", (player) => {
var vehicle = player.vehicle;
	let staticPed = mp.peds.new(mp.joaat("Orleans"), new mp.Vector3(vehicle.position.x + 2.0, vehicle.position.y + 2.0, vehicle.position.z + 2.0, 326),
    {    
          dynamic: true, // still server-side but not sync'ed anymore
          frozen: false,
          invincible: true
    },vehicle.heading);

	player.call("testy2",[staticPed]);

});

mp.events.addCommand("veh", (player) => {
  player.call("abrirTunning",[true]);
});