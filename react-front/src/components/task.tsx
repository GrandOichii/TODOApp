import { ComponentProps } from "react"
import Subtask from "./subtask"


interface TaskProps extends ComponentProps<"div"> {
    task: Task
}

const Task = (props: TaskProps) => {

    const task = props.task
    // console.log(task.title);
    
    return <>
        <h3>{task.title}</h3>
        <ul>
            {task.subtasks.map(st => (
                <li key={st.id.toString()}>
                    <Subtask subtask={st} />
                </li>
            ))}
        </ul>
    </>
}

export default Task