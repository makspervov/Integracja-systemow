import { useState, useEffect } from "react";
import axios from "axios";
import styles from "../styles/admin/styles.module.css";

const AdminPage = () => {
  const [users, setUsers] = useState([]);
  const [error, setError] = useState("");

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const response = await axios.get("http://localhost:3000/api/getUser");
      setUsers(response.data);
    } catch (error) {
      setError("Failed to fetch users.");
    }
  };

  const deleteUser = async (userId) => {
    try {
      await axios.delete(`http://localhost:3000/api/deleteUser/${userId}`);
      fetchUsers();
    } catch (error) {
      setError("Failed to delete user.");
    }
  };

  return (
    <div className={styles.admin_container}>
      <h1>User List</h1>
      {error && <div className={styles.error_msg}>{error}</div>}
      <table className={styles.user_table}>
        <thead>
          <tr>
            <th>ID</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr key={user._id}>
              <td>{user._id}</td>
              <td>{user.firstName}</td>
              <td>{user.lastName}</td>
              <td>{user.email}</td>
              <td>
                <button onClick={() => deleteUser(user._id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default AdminPage;
