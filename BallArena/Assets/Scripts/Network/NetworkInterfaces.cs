
public interface INetwork
{
  SocketIO.SocketIOComponent Server { get; }
  bool SetServer();

  byte[] OnSerializeShit();
  void OnDeserializeShit(byte[] data);
}

public interface INetworkFactory
{
  INetworkFunctionFactory CreateSocketFunction();
}