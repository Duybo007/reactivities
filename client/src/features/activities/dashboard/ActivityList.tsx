import { Box } from "@mui/material";
import ActivityCard from "./ActivityCard";

export default function ActivityList({
  activities,
  selectActivity
}: {
  activities: Activity[];
  selectActivity: (id: string) => void;
}) {
  return (
    <Box sx={{ display: "flex", flexDirection: "column", gap: 3 }}>
      {activities.map((activity) => (
        <ActivityCard
          key={activity.id}
          activity={activity}
          selectActivity={selectActivity}
        />
      ))}
    </Box>
  );
}
