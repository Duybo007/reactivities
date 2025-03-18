import { useEffect, useState } from "react";
import "./App.css";
import axios from "axios";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    axios.get<Activity[]>("https://localhost:5001/api/activities")
      .then(res => setActivities(res.data));
  }, []);

  return (
    <>
      <ul>
        {activities.map((activity) => (
          <li key={activity.id}>{activity.title}</li>
        ))}
      </ul>
    </>
  );
}

export default App;
