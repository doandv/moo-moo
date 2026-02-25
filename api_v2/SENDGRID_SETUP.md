# SendGrid Setup Guide

## Bước 1: Tạo SendGrid API Key

1. Đăng ký miễn phí tại: https://signup.sendgrid.com/
2. Sau khi đăng nhập, vào **Settings** → **API Keys**
3. Click **"Create API Key"**
4. **API Key Name**: "MooMoo App"
5. **API Key Permissions**: Chọn **"Full Access"** (hoặc chỉ "Mail Send" nếu muốn giới hạn)
6. Click **"Create & View"**
7. **Copy API Key** (bắt đầu bằng `SG.`)

⚠️ **LƯU Ý:** API Key chỉ hiện 1 lần, lưu ngay!

## Bước 2: Cấu hình appsettings.Development.json

Mở file và thay `YOUR_SENDGRID_API_KEY`:

```json
"Email": {
  "ApiKey": "SG.xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
  "FromEmail": "noreply@moomoo.app",
  "FromName": "MooMoo App"
}
```

## Bước 3: Verify Sender Email (Quan trọng!)

SendGrid yêu cầu **verify email** trước khi gửi:

### Option A: Single Sender Verification (Nhanh - Dùng cho Development)
1. Vào **Settings** → **Sender Authentication**
2. Click **"Verify a Single Sender"**
3. Điền email của bạn (có thể dùng Gmail): `doandv941@gmail.com`
4. Check email inbox → Click **"Verify Single Sender"**
5. Update `FromEmail` trong appsettings:
   ```json
   "FromEmail": "doandv941@gmail.com"
   ```

### Option B: Domain Authentication (Production)
1. Vào **Settings** → **Sender Authentication** → **Authenticate Your Domain**
2. Chọn DNS provider của bạn (Cloudflare, GoDaddy, etc.)
3. SendGrid sẽ cung cấp các DNS records (CNAME, TXT)
4. Thêm records vào DNS provider
5. Sau khi verify, dùng email với domain đó:
   ```json
   "FromEmail": "noreply@yourdomain.com"
   ```

## Bước 4: Test gửi email

```bash
POST http://localhost:5000/api/auth/register-with-email
{
  "email": "test@example.com",
  "password": "Test@123",
  "name": "Test User"
}
```

## SendGrid Free Tier

- ✅ **100 emails/day** miễn phí mãi mãi
- ✅ Không cần credit card
- ✅ Gửi được cho bất kỳ ai (sau khi verify sender)
- ✅ Email templates, analytics, tracking

## Paid Plans (Nếu cần scale)

- **Essentials**: $19.95/tháng - 50k emails
- **Pro**: $89.95/tháng - 100k emails
- Hoặc **Pay as you go**: $1/1,000 emails

## Troubleshooting

**Lỗi: "The from address does not match a verified Sender Identity"**
→ Chưa verify sender email. Làm theo Bước 3.

**Lỗi: "Unauthorized"**
→ API Key sai hoặc không có quyền "Mail Send"

**Email không đến**
→ Check Spam folder, hoặc vào **Activity** trong SendGrid dashboard để xem log

**Lỗi: "Bad Request"**
→ FromEmail hoặc ToEmail không đúng format

## Best Practices

- ✅ Dùng **Single Sender** cho development/testing
- ✅ Dùng **Domain Authentication** cho production
- ✅ Không commit API Key vào Git (đã add vào .gitignore)
- ✅ Dùng User Secrets cho local dev:
  ```bash
  dotnet user-secrets set "Email:ApiKey" "SG.your-api-key"
  ```
- ✅ Dùng Environment Variables cho production

## SendGrid Dashboard Features

- **Activity**: Xem lịch sử emails đã gửi, delivered, bounced
- **Templates**: Tạo HTML email templates đẹp
- **Email API → Integration Guide**: Code examples
- **Stats**: Open rate, click rate, bounce rate

## So sánh với các provider khác

| Provider | Free Tier | Pros | Cons |
|----------|-----------|------|------|
| **SendGrid** | 100/day | Easy setup, good docs | Free tier hơi thấp |
| Mailgun | 1,000/month | Developer-friendly | Cần verify domain |
| Gmail SMTP | 500/day | Miễn phí | Bị giới hạn, dễ bị block |
| AWS SES | 62k/month (1 năm) | Rẻ nhất | Phức tạp setup |

SendGrid là **best choice** cho startup vì:
- Setup nhanh (< 5 phút)
- Dashboard trực quan
- Docs và support tốt
- Scale dễ dàng khi cần
