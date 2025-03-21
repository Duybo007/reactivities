import { Box, Button, Paper, TextField, Typography } from "@mui/material";
import { FormEvent } from "react";
import { useActivities } from "../../../lib/hooks/useActivities";

export default function ActivityForm({
  activity,
  closeForm,
}: {
  activity?: Activity;
  closeForm: () => void;
}) {
  const { updateActivity, createActivity } = useActivities();

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const formData = new FormData(event.currentTarget);

    const data: { [key: string]: FormDataEntryValue } = {};
    formData.forEach((value, key) => {
      data[key] = value;
    });

    if (activity) {
      data.id = activity.id;
      await updateActivity.mutateAsync(data as unknown as Activity);
    } else {
      await createActivity.mutateAsync(data as unknown as Activity);
    }
    closeForm();
  };
  return (
    <Paper sx={{ borderRadius: 3, padding: 3 }}>
      <Typography variant="h5" gutterBottom color="primary">
        Create activity
      </Typography>
      <Box
        onSubmit={handleSubmit}
        component="form"
        display="flex"
        flexDirection="column"
        gap={3}
      >
        <TextField
          label="Title"
          name="title"
          defaultValue={activity?.title}
        ></TextField>
        <TextField
          label="Description"
          name="description"
          multiline
          rows={3}
          defaultValue={activity?.description}
        ></TextField>
        <TextField
          label="Category"
          name="category"
          defaultValue={activity?.category}
        ></TextField>
        <TextField
          label="Date"
          name="date"
          type="date"
          defaultValue={
            activity?.date
              ? new Date(activity.date).toISOString().split("T")[0]
              : new Date().toISOString().split("T")[0]
          }
        ></TextField>
        <TextField
          label="City"
          name="city"
          defaultValue={activity?.city}
        ></TextField>
        <TextField
          label="Venue"
          name="venue"
          defaultValue={activity?.venue}
        ></TextField>
        <Box display="flex" justifyContent="end" gap={3}>
          <Button color="inherit" onClick={closeForm}>
            Cancel
          </Button>
          <Button
            color="success"
            variant="contained"
            type="submit"
            disabled={updateActivity.isPending || createActivity.isPending}
          >
            Submit
          </Button>
        </Box>
      </Box>
    </Paper>
  );
}
