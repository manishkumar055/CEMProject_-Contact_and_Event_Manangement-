namespace ServicesLayer.RequestDTOs
{
    public class ContactUpdateDto
    {
        public ContactDto Person { get; set; } = null!;
        public ContactInfoDto[] Information { get; set; } = null!;
        public ContactAddressDto[] Addresses { get; set; } = null!;


    }
}
