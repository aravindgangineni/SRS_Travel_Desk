#!/bin/bash

echo "🚀 Starting SRS Travel Desk with JWT Authentication (Docker)"
echo "=========================================================="

# Stop any existing containers
echo "Stopping existing containers..."
docker-compose -f docker-compose.simple.yml down 2>/dev/null
docker-compose -f docker-compose.jwt.yml down 2>/dev/null
docker-compose -f docker-compose.dev.yml down 2>/dev/null

# Build and start services
echo "Building and starting services..."
docker-compose -f docker-compose.simple.yml up --build -d

# Wait for services to be ready
echo "Waiting for services to start..."
sleep 15

# Check service status
echo "Checking service status..."
docker-compose -f docker-compose.simple.yml ps

# Test JWT authentication
echo ""
echo "Testing JWT authentication..."
LOGIN_RESPONSE=$(curl -s -X POST http://localhost:5088/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"employee1@cgi.com","password":"password123"}')

if echo "$LOGIN_RESPONSE" | grep -q "token"; then
    echo "✅ JWT Authentication working!"
else
    echo "❌ JWT Authentication failed"
    echo "Response: $LOGIN_RESPONSE"
fi

echo ""
echo "🎉 SRS Travel Desk is ready!"
echo "📱 Frontend: http://localhost:4200"
echo "🔧 Backend API: http://localhost:5088"
echo "🗄️  Database: localhost:1433"
echo ""
echo "🔐 Test credentials:"
echo "Employee: employee1@cgi.com / password123"
echo "Manager: manager1@cgi.com / password123"
echo "Admin: admin1@cgi.com / password123"
echo ""
echo "🧪 Test JWT manually:"
echo "curl -X POST http://localhost:5088/api/auth/login \\"
echo "  -H \"Content-Type: application/json\" \\"
echo "  -d '{\"email\":\"employee1@cgi.com\",\"password\":\"password123\"}'"
