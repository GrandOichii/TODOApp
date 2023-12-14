import { ComponentProps, FormEvent, useRef, useState } from "react"
import Subtask from "./subtask"
import api from "../api/axios"


interface TaskProps extends ComponentProps<"article"> {
    task: Task,
    onTasksUpdated: (newTasks: Array<Task>) => void
}

const Task = (props: TaskProps) => {
    const [task, setTask] = useState(props.task)

    const removeClicked = async () => {
        // TODO ask to confirm
        const confirmed = window.confirm(`Remove task \'${task.title}\'?`)
        if (!confirmed) return

        const resp = await api.delete(`/api/tasks/${props.task.id}`)
        props.onTasksUpdated(resp.data)
    }

    const addNewSubtask = async (e: FormEvent) => {
        e.preventDefault()

        const target = e.target as typeof e.target & {
            title: { value: string },
        }

        const resp = await api.post('/api/tasks/subtask/add', {
            title: target.title.value,
            taskID: props.task.id
        })
        setTask(resp.data)

        target.title.value = ''
    }
    
    return <article style={{border: "1px solid black", padding: 10}}>
        <h3>{task.title}</h3>
        <p style={{paddingLeft: 8}}>{task.description}</p>
        <div>
            <ul>
                {task.subtasks.map(st => (
                    <li key={st.id.toString()}>
                        <Subtask ownerTaskID={task.id} subtask={st} />
                    </li>
                ))}
                <form onSubmit={addNewSubtask}>
                    <input type="text" name="title"/>
                    <input type="submit" value="Add subtask"/>
                </form>
            </ul>
        </div>
        <input type="button" value="Remove" onClick={removeClicked}/>
    </article>
}

export default Task