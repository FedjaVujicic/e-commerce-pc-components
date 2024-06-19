namespace ComponentShopAPI.Dtos
{
    class CommentDto(string firstName, string lastName, string productName, string text)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string ProductName { get; set; } = productName;
        public string Text { get; set; } = text;
    }
}
