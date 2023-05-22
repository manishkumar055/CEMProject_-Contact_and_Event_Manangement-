//using DataAccessLayer.DataServices;
//using DataAccessLayer.DBMethods;
//using DataAccessLayer.IDataServices;
//using DataAccessLayer.IDBMethods;
//using ServicesLayer.IServices;
//using ServicesLayer.Services;

using DataAccessLayer.ChangeTrack;
using DataAccessLayer.DbHelper;
using DataAccessLayer.DbMethods;
using DataAccessLayer.Email;
using DataAccessLayer.IDbMethods;
using Final_Experiment.Helper;
using SendGrid.Extensions.DependencyInjection;
using ServicesLayer.IService;
using ServicesLayer.Services;

namespace APIs.Dependencies
{
    public class AddDependencies
    {
        public static void AddDependecy(WebApplicationBuilder builder)
        {
            builder.Services.AddSendGrid(options =>
                options.ApiKey = builder.Configuration.GetSection("ApiKey").Value);
            builder.Services.AddHttpContextAccessor();

            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("RequireAdministratorRole",
            //         policy => policy.RequireRole("Admin"));
            //});

            //builder.Services.AddScoped<IContactInfoServices, ContactInfoServices>();
            //builder.Services.AddScoped<IContactAddressServices, ContactAddressServices>();
            //builder.Services.AddScoped<IContactAddressMethods,ContactAddressMethods>();
            //builder.Services.AddScoped<IContactInfoMethods, ContactInfoMethods>();
            //builder.Services.AddScoped<IChangeTracking, ChangeTracking>();
            builder.Services.AddScoped<ITokenCreation, TokenCreation>();
            builder.Services.AddScoped<IEmployeeMethods, EmployeeMethods>();
            builder.Services.AddScoped<IEventInviteeServices, EventInviteeServices>();
            builder.Services.AddScoped<IEventInviteeMethods, EventInviteeMethods>();
            builder.Services.AddScoped<IEmailInjection, EmailInjection>();
            builder.Services.AddScoped<IEventServices, EventServices>();
            builder.Services.AddScoped<IEventMethods, EventMethods>();
            builder.Services.AddScoped<IContactServices, ContactServices>();
            builder.Services.AddScoped<IContactMethods, ContactMethods>();
            builder.Services.AddScoped<IStateMethods, StateMethods>();
            builder.Services.AddScoped<IStateServices, StateServices>();
            builder.Services.AddScoped<ICounterPartyMethods, CounterPartyMethods>();
            builder.Services.AddScoped<ICounterPartyServices, CounterPartyServices>();
            builder.Services.AddScoped<IServicesHelper, ServicesHelper>();
            builder.Services.AddScoped<IChangeTracking, ChangeTracking>();


        }
    }
}
