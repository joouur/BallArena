class UnityObject{
  constructor(id, lastPosition, lastMoveTime) {
    this.ID = id;
    this.LastPosition = lastPosition;
    this.LastMoveTime = lastMoveTime;
  }
  get ID() {
    return ID;
  }
  get LastPosition(){
    return LastPosition;
  }
}