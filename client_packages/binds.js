var playerList = false;

mp.keys.bind(0x71, false, () => { // F2
    mp.events.callLocal('OpenCharacterMenu');
});

mp.keys.bind(0x1B, false, () => { // Escape
    mp.events.callRemote('ActionPressEnterOrEsc');
});

mp.keys.bind(0x0D, false, () => { // Enter
    mp.events.callRemote('ActionPressEnterOrEsc');
});

mp.keys.bind(0x54, false, () => { // T
    mp.events.callRemote('ActionPressT');
});

mp.keys.bind(0x75, false, () => { // F6
    mp.events.callRemote('ActionMenuCompany');
    mp.events.callRemote('ActionOpenBuyBusiness');
    mp.events.callRemote('ActionSignContractCompany');
    mp.events.callRemote('ActionCompanyDuty');
    mp.events.callRemote('ActionPDDuty');
});

mp.keys.bind(0x74, false, () => { // F5
    mp.events.callRemote('ActionBank');
    mp.events.callRemote('ActionEnterCompany');
    mp.events.callRemote('ActionHouse');
    mp.events.callRemote('ActionFuelGas');
    mp.events.callRemote('ActionBusinessInteract');
});

mp.keys.bind(0x73, false, () => { // F4
    mp.events.callRemote('ActionInventory');
});

mp.keys.bind(0x72, false, () => { // F3
    mp.events.callRemote('ActionMenuVehicle');
});

mp.keys.bind(0x4E, false, () => { // N
    mp.events.callRemote('ActionEngineVehicle');
    mp.events.callRemote('BoxOnTruck');
});

mp.keys.bind(0x59, false, () => { // Y
    mp.events.callRemote('ActionPickItem');
    mp.events.callRemote('ActionCargueroInteract');
});

mp.keys.bind(0x59, false, () => {
    mp.events.callRemote('PoliceDoorManager');
});

mp.keys.bind(0x55, false, () => { // U
    mp.events.callLocal('OpenPlayerList');
});
