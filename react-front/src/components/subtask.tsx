import { ChangeEvent, ComponentProps, useState } from "react"
import api from "../api/axios"

interface SubtaskProps extends ComponentProps<"div"> {
    subtask: Subtask,
    ownerTaskID: number
}

const Subtask = (props: SubtaskProps) => {
    const [subtask, setSubtask] = useState(props.subtask)

    const setCompleted = async (e: ChangeEvent<HTMLInputElement>) => {
        const resp = await api.patch('/api/tasks/subtask/setcompleted', {
            subtaskID: subtask.id,
            taskID: props.ownerTaskID,
            completed: e.target.checked
        })
        setSubtask(resp.data)
    }

    return <div style={subtask.completed ? {} : {backgroundColor: "#F5B7B1"}}>
        <input type="checkbox" onChange={setCompleted} checked={subtask.completed}/>
        <label>{subtask.title}</label>
        
    </div>
}

export default Subtask