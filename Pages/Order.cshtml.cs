using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Bakery.Data;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Bakery.Pages
{   
    
    public class OrderModel : PageModel
    {
        //inject Bakery Context
        private BakeryContext db;
        
        public OrderModel(BakeryContext db) => this.db = db;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public Product Product { get; set; }

        [BindProperty]
        public Order Order {get; set;}

      /*[BindProperty, EmailAddress, Required, Display(Name = "Your Email Address")]
        public string OrderEmail { get; set; }

        [BindProperty, Required(ErrorMessage = "Please supply a shipping address"), Display(Name = "Shipping Address")]
        public string OrderShipping { get; set; }

        [BindProperty, Display(Name = "Quantity")]
        public int OrderQuantity { get; set; } = 1;
    */
        public async Task OnGetAsync() => Product = await db.Products.FindAsync(Id);

        public async Task<IActionResult> OnPostAsync()
        {
            Product = await db.Products.FindAsync(Id);

            if (ModelState.IsValid)
            {
                
                var body = $@"<p>Thank you, we have received your order for {Order.OrderQuantity} unit(s) of {Product.Name}!</p>
                <p>Your address is: <br/>{Order.OrderShipping.Replace("\n", "<br/>")}</p>
                Your total is ${Product.Price * Order.OrderQuantity}.<br/>
                We will contact you if we have questions about your order.  Thanks!<br/>";

                var smtp = new SmtpClient()
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("billypalacio25@gmail.com", "masquinita#174"),
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                };

                var message = new MailMessage();

                message.To.Add(Order.OrderEmail);
                message.Subject = "Fourth Coffee - New Order";
                message.Body = body;
                message.IsBodyHtml = true;
                message.From = new MailAddress("sales@fourthcoffee.com");
                await smtp.SendMailAsync(message);


                /*Insert order*/        
                
                var entry = db.Orders.Add(new Order());
                entry.CurrentValues.SetValues(Order);
                await db.SaveChangesAsync();

                return RedirectToPage("OrderSuccess");
            }


            return Page();

        }





    }
}


