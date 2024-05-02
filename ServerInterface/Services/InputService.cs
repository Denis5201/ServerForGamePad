using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ServerInterface.Services
{
    public class InputService : InputController.InputControllerBase
    {
        private readonly ILogger<InputService> _logger;
        private readonly Canvas canvas;
        private readonly Rectangle rectangle;


        public InputService(ILogger<InputService> logger, Canvas myCanvas, Rectangle myRect)
        {
            _logger = logger;
            canvas = myCanvas;
            rectangle = myRect;
        }

        public override async Task<HandleResponse> Send(IAsyncStreamReader<InputRequest> requestStream, ServerCallContext context)
        {
            await foreach (InputRequest request in requestStream.ReadAllAsync())
            {
                rectangle.Dispatcher.Invoke(
                    () =>
                    {
                        var left = Canvas.GetLeft(rectangle);
                        var top = Canvas.GetTop(rectangle);
                        Canvas.SetLeft(rectangle, left + request.AxisX);
                        Canvas.SetTop(rectangle, top + request.AxisY);
                    }
                );
                var t = DateTime.UtcNow;
                Console.WriteLine($"X: {request.AxisX} Y: {request.AxisY} {t} {t.Millisecond}");
            }
            Console.WriteLine("Данные получены");
            return new HandleResponse { Success = true };
        }
    }
}
