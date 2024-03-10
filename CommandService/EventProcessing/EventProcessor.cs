using System;
using System.Text.Json;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
         
            switch (eventType)
            {
                case EventType.PlatformPublished:
                    // TO DO 
                    break;
                default:
                    break;
            }
        }

        private void addPlatform(string platformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<CommandRepo>();

                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if (repo.ExternalPlatformExists(plat.ExternalId))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                    }
                    else
                    {
                        Console.Out.WriteLine("--> Platform already exists...");
                    }
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine($"Could not add platform to DB: {ex.Message}");
                }
            }
        }
        
        private EventType DetermineEvent(string notificationMessage)
        {
            Console.Out.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.Out.WriteLine("--> Platform Published Event Detected");
                    return EventType.PlatformPublished;
                default:
                    Console.Out.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;        
            }
        }
    }

    enum EventType
    {
        PlatformPublished, 
        Undetermined,
        
    }

}