using Grpc.Core;
using RemoteController;

namespace RemoteController.Services
{
    public class InputService : InputController.InputControllerBase
    {
        private readonly ILogger<InputService> _logger;
        public InputService(ILogger<InputService> logger)
        {
            _logger = logger;
        }

        public override async Task<HandleResponse> Send(IAsyncStreamReader<InputRequest> requestStream, ServerCallContext context)
        {
            await foreach (InputRequest request in requestStream.ReadAllAsync())
            {
                Console.WriteLine($"X: {request.AxisX} Y: {request.AxisY}");
            }
            Console.WriteLine("Данные получены");
            return new HandleResponse { Success = true };
        }
    }
}
