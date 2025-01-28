# StripeBlazorApp

A simple Blazor server-side application that integrates with the Stripe API for creating payment sessions. This app demonstrates how to use Stripe's checkout functionality for e-commerce scenarios.

## Features

- Display products with name and price.
- "Buy Now" button triggers Stripe Checkout.
- Integration with Stripe API for session creation.
- Configurable success and cancel URLs.

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/Vexmage/StripeBlazorApp.git
   cd StripeBlazorApp

Add your Stripe test API key in the appsettings.json file:

{
  "Stripe": {
    "SecretKey": "sk_test_your_test_key_here"
  }
}

Run the application:

dotnet run --launch-profile https

Open your browser and navigate to:

    https://localhost:44386

How It Works

    The main page displays a list of products with a "Buy Now" button.
    Clicking "Buy Now" sends a POST request to the /api/payments/create-checkout-session endpoint.
    The Stripe session is created on the server-side, and the user is redirected to Stripe Checkout for payment.
    Success or cancellation redirects the user to predefined URLs.

Notes

    This app is in test mode, so no real payments will be processed.


Folder Structure

    Pages: Contains Razor components for the UI.
    Services: Includes StripeService for handling API integration.
    Controllers: API controllers for handling requests.
    Models: Application models like Product and StripeSessionResponse.

License

This project is licensed under the MIT License. Feel free to use and modify it as needed.


---

### **Steps to Add This README**
1. Create a new file named `README.md` in the root directory of your project.
2. Copy and paste the content above into the file.
3. Commit and push the file to GitHub:
   ```bash
   git add README.md
   git commit -m "Add README.md"
   git push origin main

