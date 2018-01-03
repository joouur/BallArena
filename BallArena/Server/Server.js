var io = require('socket.io')(process.env.PORT || 3000);
var shortid = require('shortid');

console.log('server started');

var playersInGame = [];

var playerSpeed = 3;
class UnityObj{
  constructor(id, lastPosition, lastMoveTime) {
    this.ID = id;
    this.LastPosition = lastPosition;
    this.LastMoveTime = lastMoveTime;
  }
}

function OnMoving(player, data) {
  data.id = player.ID;
  console.log('client moved', JSON.stringify(data));
    
  player.Destination(data.d);
  console.log("distance between current and destination: ", lineDistance(data.c, data.d));
    
  var elapsedTime = Date.now() - player.lastMoveTime;
  var travelDistanceLimit = elapsedTime * playerSpeed / 1000;
  var requestedDistanceTraveled = lineDistance(player.lastPosition, data.c);
   
  console.log("travelDistanceLimit:", travelDistanceLimit, "requestedDistanceTraveled", requestedDistanceTraveled);
   
  if(requestedDistanceTraveled > travelDistanceLimit)
    //we know they are cheating

  player.lastMoveTime = Date.now();
  player.lastPosition = data.c;
    
  delete data.c;
  data.x = data.d.x;
  data.y = data.d.y;
  data.z = data.d.z;
  delete data.d;
  socket.broadcast.emit('OnMove', data);
}
class Player extends UnityObj{
  constructor(id, lastPosition, lastMoveTime, destination) {
    super(id, lastPosition, lastMoveTime);
    this.Destination = destination;
  }
}
function SpawnPlayers(SocketToEmit, newID, playersToSpawn){
  for(var id in playersToSpawn)
  {
    if(newID == id)
    { continue; }
    SocketToEmit.emit('OnSpawn', playersToSpawn[id]);
    console.log('Sending spawn to new player for id:', id);
  }
}

function DoNewPlayer(SocketToEmit, newID){
  
  var player = new Player(newID, {x: 0, y: 0, z:0}, {x: 0, y: 0, z:0}, 0);
  console.log('Created new player for id:', newID);
  
  SocketToEmit.emit('OnRegister', {ID: newID});
  SocketToEmit.broadcast.emit('OnSpawn', {ID: newID});
  SocketToEmit.broadcast.emit('OnRequestTransform');
  
  return player;
}

function lineDistance(vectorA, vectorB) {
  var xs = 0;
  var ys = 0;
  
  xs = vectorB.x - vectorA.x;
  xs = xs * xs;
  
  ys = vectorB.y - vectorA.y;
  ys = ys * ys;
  
  zs = vectorB.z - vectorA.z;
  zs = zs * zs;

  return Math.sqrt( xs + ys + zs);
}

io.on('connection', function(socket){
  var thisPlayerId = shortid.generate();  
  
  playersInGame[thisPlayerId] = DoNewPlayer(socket, thisPlayerId);

  console.log('Client connected, broadcasting spawn, id:', thisPlayerId);
  
  SpawnPlayers(socket, thisPlayerId, playersInGame)

  socket.on('OnUpdateTransform', function(data) {
    data.ID = thisPlayerId;
    console.log('Updating x:', data.dx);
    socket.broadcast.emit('OnUpdateTransform', data);
  });
  socket.on('OnDisconnect', function(){
    console.log('Client Disconnected');
    delete players[thisPlayerId];
    socket.broadcast.emit('OnDisconnect', { id: thisPlayerId });
  });
})