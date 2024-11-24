import { useEffect, useState } from "react";
import { Container, Row, Col, Image, Button } from "react-bootstrap";
import { getCookie } from "../../helpers/helpers";

const Profile = () => {
  const [user, setUser] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchUserProfile = async () => {
      const accessToken = getCookie("AuthToken");
      if (!accessToken) {
        setError("No access token found");
        return;
      }

      try {
        const response = await fetch("/api/account/profile", {
          method: "GET",
          headers: {
            Authorization: `Bearer ${accessToken}`,
            "Content-Type": "application/json",
          },
        });

        if (!response.ok) {
          throw new Error("Failed to fetch user profile");
        }

        const userProfile = await response.json();
        setUser(userProfile);
      } catch (error) {
        setError(error.message);
      }
    };

    fetchUserProfile();
  }, []);

  if (error) {
    return <div>Error: {error}</div>;
  }

  if (!user) {
    return <div>Loading...</div>;
  }

  return (
    <Container>
      <Row>
        <Col md={12}>
          <Row>
            <h2>User Profile</h2>
            <Col md={2}>
              <Image
                src={user.profileImage}
                alt="profile image"
                rounded
                fluid
              />
            </Col>
            <Col md={4}>
              <p>
                <i>🪪</i> {user.fullName}
              </p>
              <p>
                <i>👤</i> {user.username}
              </p>
              <p>
                <i>✉️</i> {user.email}
              </p>
              <p>
                <i>📱</i> {user.phoneNumber}
              </p>
            </Col>
          </Row>
        </Col>
      </Row>
      <form action="/api/account/logout" method="post">
        <Button type="submit" variant="primary">
          Logout
        </Button>
      </form>
    </Container>
  );
};

export { Profile };
