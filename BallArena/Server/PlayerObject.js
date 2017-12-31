class Player extends UnityObject{
  constructor(id, lastPosition, lastMoveTime, destingation) {
    super(id, lastPosition, lastMoveTime);
    this.Destination = destination;
  }
  set Destination(value){
    try {
      LastPosition = Destination;
    }
    catch { 
      console.log('Could not set last position for player id: ', this.ID);
    }

    Destination.x = value.x;
    Destination.y = value.y;
    Destination.z = value.z;
  }
}
function SpawnPlayers(ID, playersToSpawn, SocketToEmit){
  for(var id in playersToSpawn)
  {
    if(ID == id)
    { continue; }
    SocketToEmit.emit('Spawn', playersToSpawn[id]);
    console.log('Sending spawn to new player for id: ', id);
  }
}

function DoNewPlayer(SocketToEmit, newID){
  
  var player = new Player(newID, {x: 0, y: 0, z:0}, {x: 0, y: 0, z:0}, 0);
  
  socket.emit('register', {ID: newID});
  socket.broadcast.emit('spawn', {ID: newID});
  socke.broadcast.emit('RequestPosition');
  
  return player;
}