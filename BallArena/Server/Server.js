var io = require('socket.io')(process.env.PORT || 3000);
var shortid = require('shortid');

console.log('server started');

var playersInGame = [];

var playerSpeed = 3;

io.on('connection', function(socket){
  var thisPlayerId = shortid.generate();  
  
  players[thisPlayerId] = DoNewPlayer(socket, thisPlayerId);

  console.log('Client connected, broadcasting spawn, id:', thisPlayerId);
  
  SpawnPlayers(playersInGame, socket);


  socket.on('OnDisconnect', function(){
    console.log('Client Disconnected');
  })
})