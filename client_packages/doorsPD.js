mp.game.object.doorControl(-1320876379, 446.5728, -980.0106, 30.8393, true, 0, 10, 0);
mp.game.object.doorControl(185711165, 450.1041, -81.4915, 30.8393, true, 0, 10, 0);
mp.game.object.doorControl(185711165, 450.1041, -984.0915, 30.8393, true, 0, 10, 0);
mp.game.object.doorControl(749848321, 453.0793, -983.1895, 30.83926, true, 0, 10, 0);
mp.game.object.doorControl(1557126584, 450.1041, -985.7384, 30.8393, true, 0, 10, 0);
mp.game.object.doorControl(-2023754432, 452.6248, -987.3626, 30.8393, true, 0, 10, 0);
mp.game.object.doorControl(749848321, 461.2865, -985.3206, 30.83926, true, 0, 10, 0);
mp.game.object.doorControl(-2023754432, 452.6248, -987.3626, 30.8393, true, 0, 10, 0);
mp.game.object.doorControl(185711165, 443.4078, -989.4454, 30.8393, true, 0, 10, 0);
mp.game.object.doorControl(185711165, 446.0079, -989.4454, 30.8393, true, 0, 10, 0);
mp.game.object.doorControl(-131296141, 443.0298, -991.941, 30.8393, true, 0, 10, 0);
mp.game.object.doorControl(-131296141, 443.0298, -994.5412, 30.8393, true, 0, 10, 0);
mp.game.object.doorControl(-1033001619, 463.4782, -1003.538, 25.00599, true, 0, 10, 0);
mp.game.object.doorControl(631614199, 461.8065, -994.4086, 25.06443, true, 0, 10, 0);
mp.game.object.doorControl(631614199, 461.8065, -997.6583, 25.06443, true, 0, 10, 0);
mp.game.object.doorControl(631614199, 461.8065, -1001.302, 25.06443, true, 0, 10, 0);
mp.game.object.doorControl(631614199, 464.5701, -992.6641, 25.06443, true, 0, 10, 0);

mp.events.add('open_door_police', () => {
    mp.game.object.doorControl(320433149, 434.7479, -983.2151, 30.83926, false, 0, 10, 0);
    mp.game.object.doorControl(-1215222675, 434.7479, -980.6184, 30.83926, false, 0, 10, 0);
});

mp.events.add('close_door_police', () => {
    mp.game.object.doorControl(320433149, 434.7479, -983.2151, 30.83926, true, 0, 10, 0);
    mp.game.object.doorControl(-1215222675, 434.7479, -980.6184, 30.83926, true, 0, 10, 0);
});

mp.events.add('open_door_police_back', () => {
    mp.game.object.doorControl(-2023754432, 469.9679, -1014.452, 26.53623, false, 0, 10, 0);
    mp.game.object.doorControl(-2023754432, 467.3716, -1014.452, 26.53623, false, 0, 10, 0);
});

mp.events.add('close_door_police_back', () => {
    mp.game.object.doorControl(-2023754432, 469.9679, -1014.452, 26.53623, true, 0, 10, 0);
    mp.game.object.doorControl(-2023754432, 467.3716, -1014.452, 26.53623, true, 0, 10, 0);
});

mp.events.add('open_door_police_cell_main', () => {
    mp.game.object.doorControl(-1033001619, 463.4782, -1003.538, 25.00599, false, 0, 10, 0);
});

mp.events.add('close_door_police_cell_main', () => {
    mp.game.object.doorControl(-1033001619, 463.4782, -1003.538, 25.00599, true, 0, 10, 0);
});

mp.events.add('open_door_police_cell_1', () => {
    mp.game.object.doorControl(631614199, 461.8065, -994.4086, 25.06443, false, 0, 10, 0);
});

mp.events.add('close_door_police_cell_1', () => {
    mp.game.object.doorControl(631614199, 461.8065, -994.4086, 25.06443, true, 0, 10, 0);
});

mp.events.add('open_door_police_cell_2', () => {
    mp.game.object.doorControl(631614199, 461.8065, -997.6583, 25.06443, false, 0, 10, 0);
});

mp.events.add('close_door_police_cell_2', () => {
    mp.game.object.doorControl(631614199, 461.8065, -997.6583, 25.06443, true, 0, 10, 0);
});

mp.events.add('open_door_police_cell_3', () => {
    mp.game.object.doorControl(631614199, 461.8065, -1001.302, 25.06443, false, 0, 10, 0);
});

mp.events.add('close_door_police_cell_3', () => {
    mp.game.object.doorControl(631614199, 461.8065, -1001.302, 25.06443, true, 0, 10, 0);
});

mp.events.add('open_door_police_cell_back', () => {
    mp.game.object.doorControl(631614199, 464.5701, -992.6641, 25.06443, false, 0, 10, 0);
});

mp.events.add('close_door_police_cell_back', () => {
    mp.game.object.doorControl(631614199, 464.5701, -992.6641, 25.06443, true, 0, 10, 0);
});

mp.events.add('open_door_police_office', () => {
    mp.game.object.doorControl(-1320876379, 446.5728, -980.0106, 30.8393, false, 0, 10, 0);
});

mp.events.add('close_door_police_office', () => {
    mp.game.object.doorControl(-1320876379, 446.5728, -980.0106, 30.8393, true, 0, 10, 0);
});

mp.events.add('open_door_police_armor_double', () => {
    mp.game.object.doorControl(185711165, 450.1041, -984.0915, 30.8393, false, 0, 10, 0);
    mp.game.object.doorControl(185711165, 450.1041, -981.4915, 30.8393, false, 0, 10, 0);
});

mp.events.add('close_door_police_armor_double', () => {
    mp.game.object.doorControl(185711165, 450.1041, -984.0915, 30.8393, true, 0, 10, 0);
    mp.game.object.doorControl(185711165, 450.1041, -981.4915, 30.8393, true, 0, 10, 0);
});

mp.events.add('open_door_police_armor', () => {
    mp.game.object.doorControl(749848321, 453.0793, -983.1895, 30.83926, false, 0, 10, 0);
});

mp.events.add('close_door_police_armor', () => {
    mp.game.object.doorControl(749848321, 453.0793, -983.1895, 30.83926, true, 0, 10, 0);
});

mp.events.add('open_door_police_locker', () => {
    mp.game.object.doorControl(1557126584, 450.1041, -985.7384, 30.8393, false, 0, 10, 0);
});

mp.events.add('close_door_police_locker', () => {
    mp.game.object.doorControl(1557126584, 450.1041, -985.7384, 30.8393, true, 0, 10, 0);
});

mp.events.add('open_door_police_locker1', () => {
    mp.game.object.doorControl(-2023754432, 452.6248, -987.3626, 30.8393, false, 0, 10, 0);
});

mp.events.add('close_door_police_locker1', () => {
    mp.game.object.doorControl(-2023754432, 452.6248, -987.3626, 30.8393, true, 0, 10, 0);
});

mp.events.add('open_door_police_roof', () => {
    mp.game.object.doorControl(749848321, 461.2865, -985.3206, 30.83926, false, 0, 10, 0);
});

mp.events.add('close_door_police_roof', () => {
    mp.game.object.doorControl(749848321, 461.2865, -985.3206, 30.83926, true, 0, 10, 0);
});

mp.events.add('open_door_police_roof1', () => {
    mp.game.object.doorControl(-340230128, 464.3613, -984.678, 43.83443, false, 0, 10, 0);
});

mp.events.add('close_door_police_roof1', () => {
    mp.game.object.doorControl(-340230128, 464.3613, -984.678, 43.83443, true, 0, 10, 0);
});

mp.events.add('open_door_police_brief', () => {
    mp.game.object.doorControl(185711165, 443.4078, -989.4454, 30.8393, false, 0, 10, 0);
    mp.game.object.doorControl(185711165, 446.0079, -989.4454, 30.8393, false, 0, 10, 0);
});

mp.events.add('close_door_police_brief', () => {
    mp.game.object.doorControl(185711165, 443.4078, -989.4454, 30.8393, true, 0, 10, 0);
    mp.game.object.doorControl(185711165, 446.0079, -989.4454, 30.8393, true, 0, 10, 0);
});

mp.events.add('open_door_police_brief1', () => {
    mp.game.object.doorControl(-131296141, 443.0298, -991.941, 30.8393, false, 0, 10, 0);
    mp.game.object.doorControl(-131296141, 443.0298, -994.5412, 30.8393, false, 0, 10, 0);
});

mp.events.add('close_door_police_brief1', () => {
    mp.game.object.doorControl(-131296141, 443.0298, -991.941, 30.8393, true, 0, 10, 0);
    mp.game.object.doorControl(-131296141, 443.0298, -994.5412, 30.8393, true, 0, 10, 0);
});

mp.events.add('open_door_police_gate', () => {
    mp.game.object.doorControl(-1603817716, 488.8923, -1011.67, 27.14583, false, 0, 10, 0);
});

mp.events.add('close_door_police_gate', () => {
    mp.game.object.doorControl(-1603817716, 488.8923, -1011.67, 27.14583, true, 0, 10, 0);
});
