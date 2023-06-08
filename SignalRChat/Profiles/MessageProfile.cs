using AutoMapper;
using SignalRChat.Entities;
using SignalRChat.Models;

namespace SignalRChat.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageModel>().ForMember(model => model.UserName, opt => opt.MapFrom(ent => ent.user.Name)); 
            CreateMap<MessageModel, Message>();
             

        }
    }
}
