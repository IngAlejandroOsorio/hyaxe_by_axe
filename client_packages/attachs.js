mp.events.add('Object.AttachTo', (objectName, playerId, boneId) => {
    var player = mp.players.atRemoteId(playerId);

    var object = mp.objects.new(mp.game.joaat(objectName), player.position,
        {
            rotation: player.rotation,
            alpha: 255,
            dimension: 0
        });

    var boneIndex = player.getBoneIndex(boneId);
    object.attachTo(player.handle, boneIndex, 0.20, 0.1, 0.25, 0, 90, 90, true, false, false, false, 0, true);
});

mp.events.add('Object.Delete', (player) => {
    var player = mp.players.atRemoteId(player);
    mp.objects.forEach((object) => {
        if (object.getAttachedTo() === player.handle) {
            object.destroy();
       return;
        }
    })
})
