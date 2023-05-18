using DataModels;
using DataModels.Dtos;

namespace Helpers
{
    public static class EmailHelper
    {
        public static string CreateOrderHtml(CustomerDto customer, List<ProductDto> products, int orderID, string title)
        {
            var result = string.Empty;

            if (customer == null || !(products?.Any() ?? false))
                return result;

            var finalPrice = default(decimal);

            result += $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
	<meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
	<style>
		body {{
			font-family: Arial, sans-serif;
			margin: 0;
			padding: 0;
			background-color: #f5f5f5;
			color: #333;
			line-height: 1.5;
		}}

        p {{
          margin: 0 0 20px;
        }}

		.container {{
			max-width: 700px;
			margin: 0 auto;
			background-color: #fff;
			box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
			padding: 20px;
		}}

		h1 {{
			text-align: center;
			margin-top: 0;
		}}

		table {{
			width: 100%;
			border-collapse: collapse;
			margin-bottom: 20px;
		}}

		table th {{
			padding: 10px;
			text-align: center;
			border-bottom: 1px solid #ddd;
		}}

		table td {{
			padding: 10px;
			text-align: left;
			vertical-align: middle;
			border-bottom: 1px solid #ddd;
		}}

		.product-image {{
			display: block;
			margin-left: auto;
			margin-right: auto;
			max-width: 200px;
			max-height: 150px;
		}}

		.product-name {{
			margin-top: 0;
			margin-bottom: 5px;
			vertical-align: middle;
		}}

		.product-description {{
			margin-top: 0;
			margin-bottom: 5px;
			font-size: 14px;
		}}

		.product-price {{
			font-weight: bold;
			font-size: 16px;
			width: 110px;
			text-align: center;
		}}

		.order-total {{
			text-align: right;
			font-weight: bold;
			margin-bottom: 20px;
			font-size: 16px;
		}}

		a {{
			text-decoration: none;
			color: #008CBA;
		}}

		@media only screen and (max-width: 600px) {{
			.product-description {{
				display: none;
			}}
			.product-name {{
				text-align: center;
			}}
		}}
	</style>
</head>
<body>
	<div class=""container"">
		<h1>{title}</h1>
		<p><strong>Name:</strong> {customer.Name} <span style=""float: right;"" ><strong>Order ID:</strong> {orderID} </span></p> 
		<table>
			<tr>
				<th>Product</th>
				<th>Description</th>
				<th>Price</th>
			</tr>";

            foreach (var product in products)
            {
                finalPrice += (product.Quantity ?? 0) * product.Price;
                result += $@"<tr>
				<td>
					<a href=""https://eshop.myportofolio.eu/Products/Details/{product.Id}"" target=""_blank""><img src=""{product.Image}"" alt=""{product.Name}"" class=""product-image""></a>
				</td>
				<td>
					<h3 class=""product-name"">{product.Name}</h3>
					<p class=""product-description"">{product.Description}</p>
				</td>
				<td class=""product-price"">{product.Quantity ?? 0} X {product.Price} €</td>
			</tr>";
            }

            result += $@"</table>
		<div class=""order-total"">
			Total: {finalPrice} €
		</div>
		<p>If you need any further assistance feel free to use our 
			<a href=""https://eshop.myportofolio.eu/Home/Contact"" target=""_blank"">Contact Page</a>
		</p>
	</div>
</body>
</html>";

            return result;
        }

        public static string NewUserCreatedHtml(CustomerDto customer, UserDto user, string title)
        {
            var result = string.Empty;

            if (customer == null || user == null)
                return result;

            result += $@"<!DOCTYPE html>
<html>
<head>
  <meta charset=""utf-8"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
  <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
  <style>
    body {{
      background-color: #f2f2f2;
      margin: 0;
      padding: 0;
      font-family: Arial, sans-serif;
      font-size: 16px;
      line-height: 1.5;
      color: #333;
    }}

    p {{
      margin: 0 0 20px;
    }}
    
    .container {{
      max-width: 600px;
      margin: 0 auto;
      padding: 40px;
      background-color: #fff;
      border-radius: 4px;
      box-shadow: 0 0 10px rgba(0,0,0,.1);
    }}
    
    .heading {{
      font-size: 24px;
      margin: 0 0 20px;
      color: #333;
      text-align: center;
    }}
    
    table {{
      width: 100%;
      border-collapse: collapse;
      margin: 0 0 20px;
    }}
    
    table td, table th {{
      padding: 12px;
      border: 1px solid #ddd;
      text-align: left;
    }}
    
    table th {{
      background-color: #f2f2f2;
    }}
    
    .button {{
      display: inline-block;
      padding: 12px 20px;
      border: none;
      border-radius: 4px;
      background-color: #333;
      color: #fff;
      font-size: 16px;
      text-align: center;
      text-decoration: none;
      cursor: pointer;
    }}
    
    .button:hover {{
      background-color: #555;
    }}
    
    @media only screen and (max-width: 600px) {{
      .container {{
        padding: 20px;
      }}
    }}
  </style>
</head>
<body>
  <div class=""container"">
    <h1 class=""heading"">{title}</h1>
    <p>Hello {customer.Name},</p>
    <p>A new user has been created with the following credentials:</p>
    <table>
      <tr>
        <th>Username</th>
        <td>{user.Email}</td>
      </tr>
      <tr>
        <th>Password</th>
        <td>{user.Password}</td>
      </tr>
    </table>
    <p>Please keep this information secure and do not share it with anyone.</p>
    <p>Thank you,</p>
    <p>The Admin Team</p>
    <p style=""text-align: center;""><a href=""https://eshop.myportofolio.eu/Users/Login"" target=""_blank"" class=""button"">Login to Dashboard</a></p>
  </div>
</body>
</html>
";

            return result;
        }
        public static string PasswordResetHtml(UserDto user, string title)
        {
            var result = string.Empty;

            if (user == null)
                return result;

            var link = $"https://eshop.myportofolio.eu/Users/ResetPassword?userID={user.Id}&token={user.PasswordResetToken}";

            result += $@"<!DOCTYPE html>
<html>
<head>
  <meta charset=""utf-8"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
  <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
  <style>
    body {{
      background-color: #f2f2f2;
      margin: 0;
      padding: 0;
      font-family: Arial, sans-serif;
      font-size: 16px;
      line-height: 1.5;
      color: #333;
    }}
    
    .container {{
      max-width: 600px;
      margin: 0 auto;
      padding: 40px;
      background-color: #fff;
      border-radius: 4px;
      box-shadow: 0 0 10px rgba(0,0,0,.1);
    }}
    
    .heading {{
      font-size: 24px;
      margin: 0 0 20px;
      color: #333;
      text-align: center;
    }}
    
    p {{
      margin: 0 0 20px;
    }}
    
    .button {{
      display: inline-block;
      padding: 12px 20px;
      border: none;
      border-radius: 4px;
      background-color: #333;
      color: #fff;
      font-size: 16px;
      text-align: center;
      text-decoration: none;
      cursor: pointer;
    }}
    
    .button:hover {{
      background-color: #555;
    }}
    
    @media only screen and (max-width: 600px) {{
      .container {{
        padding: 20px;
      }}
    }}
  </style>
</head>
<body>
  <div class=""container"">
    <h1 class=""heading"">{title}</h1>
    <p>Hello,</p>
    <p>We have received a request to reset your password. If you did not request this, please ignore this email.</p>
    <p>To reset your password, click the button below:</p>
    <p><a href=""{link}"" target=""_blank"" class=""button"">Reset Password</a></p>
    <p>If the button above does not work, you can click or copy and paste the following link into your browser's address bar:</p>
    <p><a href=""{link}"" target=""_blank"">{link}</a></p>
    <p>If you have any questions or need further assistance, please contact our support team.</p>
    <p>Thank you,</p>
    <p>The Admin Team</p>
  </div>
</body>
</html>";

            return result;
        }
        public static string ContactMessageHtml(string name, string email, string message)
        {
            var result = string.Empty;

            result += $@"<!DOCTYPE html>
<html>
<head>
  <meta charset=""utf-8"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
  <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
  <style>
    body {{
      font-family: Arial, sans-serif;
      background-color: #f8f9fa;
      color: #333;
      padding: 20px;
    }}
    h2 {{
      color: #007bff;
      margin-bottom: 20px;
    }}
    p {{
      margin-bottom: 10px;
    }}
    .container {{
      max-width: 600px;
      margin: 0 auto;
      background-color: #ffffff;
      border-radius: 10px;
      box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
      padding: 30px;
    }}
    .form-container {{
      margin-top: 20px;
    }}
    .form-group {{
      margin-bottom: 20px;
    }}
    .form-group label {{
      display: block;
      font-weight: bold;
      margin-bottom: 5px;
      color: #555;
    }}
  </style>
</head>
<body>
  <div class=""container"">
    <h2>Hello {name},</h2>
    <p>We have successfully received your message and our team will get back to you as soon as possible.</p>
    <div class=""form-container"">
      <h3>Contact Information</h3>
      <div class=""form-group"">
        <label>Email:</label>
        <div>{email}</div>
      </div>
      <div class=""form-group"">
        <label>Message:</label>
        <div>{message}</div>
      </div>
    </div>
    <p>Thank you for contacting us.</p>
    <p>Best regards,</p>
    <p>The Admin Team</p>
  </div>
</body>
</html>";

            return result;
        }
    }
}