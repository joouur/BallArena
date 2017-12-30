var io = require('socket.io')(process.env.PORT || 3000);
var shortid = require('shortid');

console.log('server started');

var players = [];

var playerSpeed = 3;

io.on('connection', function(socket){
  var thisPlayerId = shortid.generate();
  
  var player = {
      id: thisPlayerId,
      destination: {
      x: 0, 
      y: 0
      },
      lastPosition: {
          x: 0,
          y: 0
      },
      lastMoveTime: 0
  };
  players[thisPlayerId] = player;
  
  console.log('Client connected');

  socket.broadcast.emit('spawn');
  playerCount++;
  for(i = 0; i < playerCount; ++i)
  {
    socket.emit('spawn');
    console.log('Sending spawn to new Player');
  }

  socket.on('move', function(data){
    console.log('Client moved');    
  })

  socket.on('disconnect', function(){
    console.log('Client Disconnected');
    playerCount--;
  })
})