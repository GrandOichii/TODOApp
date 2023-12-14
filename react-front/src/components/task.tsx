import { ComponentProps } from "react"
import Subtask from "./subtask"
import api from "../api/axios"


interface TaskProps extends ComponentProps<"article"> {
    task: Task,
    onTasksUpdated: (newTasks: Array<Task>) => void
}

const Task = (props: TaskProps) => {

    const task = props.task
    // console.log(task.title);

    const removeClicked = async () => {
        // TODO ask to confirm
        const resp = await api.delete(`/api/tasks/${props.task.id}`)
        props.onTasksUpdated(resp.data)
    }
    
    return <article style={{border: "1px solid black", padding: 10}}>
        <h3>{task.title}</h3>
        <p style={{paddingLeft: 8}}>{task.description}</p>
        <ul>
            {task.subtasks.map(st => (
                <li key={st.id.toString()}>
                    <Subtask ownerTaskID={task.id} subtask={st} />
                </li>
            ))}
        </ul>
        <input type="button" value="Remove" onClick={removeClicked}/>
    </article>
}

export default Task