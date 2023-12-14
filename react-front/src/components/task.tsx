import { ComponentProps } from "react"
import Subtask from "./subtask"


interface TaskProps extends ComponentProps<"article"> {
    task: Task
}

const Task = (props: TaskProps) => {

    const task = props.task
    // console.log(task.title);
    
    return <article>
        <h3>{task.title}</h3>
        <p style={{paddingLeft: 8}}>{task.description}</p>
        <ul>
            {task.subtasks.map(st => (
                <li key={st.id.toString()}>
                    <Subtask ownerTaskID={task.id} subtask={st} />
                </li>
            ))}
        </ul>
    </article>
}

export default Task