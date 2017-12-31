var io = require('socket.io')(process.env.PORT || 3000);
var shortid = require('shortid');

console.log('server started');

var playersInGame = [];

var playerSpeed = 3;

io.on('connection', function(socket){
  var thisPlayerId = shortid.generate();
  
  var player = new Player(thisPlayerId, {x: 0, y: 0, z:0}, {x: 0, y: 0, z:0}, 0);
  players[thisPlayerId] = player;
  
  console.log('Client connected, broadcasting spawn, id:', thisPlayerId);

  socket.emit('Register', {ID: thisPlayerId});
  socket.broadcast.emit('Spawn', {ID: thisPlayerId});
  socke.broadcast.emit('RequestPosition');
  
  player.SpawnPlayers(playersInGame);


  socket.on('disconnect', function(){
    console.log('Client Disconnected');
    playerCount--;
  })
})