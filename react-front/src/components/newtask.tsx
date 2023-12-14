import { ComponentProps, FormEvent } from "react";
import api from "../api/axios";

interface NewTaskProps extends ComponentProps<"div"> {
    onAdded: (newTasks: Array<Task>) => void
}

const NewTask = (props: NewTaskProps) => {

    const onSubmit = async (e: FormEvent) => {
        e.preventDefault()

        // TODO
        const target = e.target as typeof e.target & {
            title: { value: string },
            description: { value: string },
        }

        const resp = await api.post('/api/tasks/create', {
            title: target.title.value,
            description: target.description.value
        })
        
        props.onAdded(resp.data)
    }

    return <form onSubmit={onSubmit} style={{padding: 5}}>
        <div>
            <label>Title: </label>
            <input type="text" name="title" required />
        </div>
        <label>Description: </label>
        <div>
            <textarea name="description" rows={5} required />
        </div>
        <input type="submit" value="New task" />
    </form>
}

export default NewTask