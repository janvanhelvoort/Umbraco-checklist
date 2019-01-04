namespace Our.Umbraco.Checklist.Mapping.Profile
{
    using AutoMapper;

    public class TaskProfileProfile : Profile
    {
        public TaskProfileProfile()
        {
            CreateMap<Models.Pocos.Task, Models.Task>()
                .ForMember(task => task.Id, expression => expression.MapFrom(item => item.Id))
                .ForMember(task => task.ContentNodeId, expression => expression.MapFrom(item => item.ContentNodeId))
                .ForMember(task => task.Content, expression => expression.MapFrom(item => item.Content))
                .ForMember(task => task.IsReady, expression => expression.MapFrom(item => item.IsReady))
                .ForMember(task => task.IsDeleted, expression => expression.MapFrom(item => item.IsDeleted));

            CreateMap<Models.Task, Models.Pocos.Task>()
                .ForMember(task => task.Id, expression => expression.MapFrom(item => item.Id))
                .ForMember(task => task.ContentNodeId, expression => expression.MapFrom(item => item.ContentNodeId))
                .ForMember(task => task.Content, expression => expression.MapFrom(item => item.Content))
                .ForMember(task => task.IsReady, expression => expression.MapFrom(item => item.IsReady))
                .ForMember(task => task.IsDeleted, expression => expression.MapFrom(item => item.IsDeleted));
        }
    }
}
