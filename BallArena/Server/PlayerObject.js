class Player {
  constructor(id, destingation, lastPosition, lastMoveTime) {
    this.ID = id;
    this.Destination = destination;
    this.LastPosition = lastPosition;
    this.LastMoveTime = lastMoveTime;
  }
  SpawnPlayers(playersToSpawn){
    for(var id in playersToSpawn)
    {
      if(this.ID == id)
      { continue; }
      socket.emit('Spawn', playersToSpawn[id]);
      console.log('Sending spawn to new player for id: ', id);
    }
  }
}