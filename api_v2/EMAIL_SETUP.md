# Email Verification Setup

## SendGrid (All Environments)

We use **SendGrid** for all environments (development, staging, production).

### Configuration

```json
// appsettings.Development.json
"Email": {
  "ApiKey": "SG.xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
  "FromEmail": "noreply@moomoo.app",
  "FromName": "MooMoo App"
}
```

### Setup Instructions

See [SENDGRID_SETUP.md](SENDGRID_SETUP.md) for detailed instructions.

**Quick Start:**
1. Sign up at https://signup.sendgrid.com/
2. Create API Key (Settings → API Keys)
3. Verify sender email (Settings → Sender Authentication)
4. Update `Email:ApiKey` in appsettings.Development.json

### Free Tier

- ✅ **100 emails/day** forever free
- ✅ No credit card required
- ✅ Full features (templates, analytics, API)

### Why SendGrid?

- **Easy Setup**: No SMTP configuration needed
- **Reliable**: 99.99% uptime SLA
- **Scalable**: From 100 emails/day → millions
- **Great Docs**: Clear API documentation
- **Dashboard**: Track email delivery, opens, clicks
- **Production Ready**: Used by Airbnb, Uber, Spotify

## Email Verification Flow

1. User registers → Token generated (GUID)
2. Token stored in `User.EmailVerificationToken` (expires in 24h)
3. Email sent with link: `/api/auth/verify-email?token={token}`
4. User clicks link → `VerifyEmailUseCase` validates token
5. User status: `PENDING_VERIFICATION` → `ACTIVE`

## Testing

```bash
POST http://localhost:5000/api/auth/register-with-email
{
  "email": "test@example.com",
  "password": "Test@123",
  "name": "Test User"
}
```

Check email inbox (including Spam folder) for verification link.
