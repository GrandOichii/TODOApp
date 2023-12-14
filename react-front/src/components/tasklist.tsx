import { useEffect, useState } from "react"
import api from "../api/axios"
import Task from "./task"
import NewTask from "./newtask"


const TaskList = () => {
    const [tasks, setTasks] = useState<Array<Task>>([])
    
    useEffect(() => {
        const fetchTasks = async () => {
            const resp = await api.get('/api/tasks')
            setTasks(resp.data)
        } 

        fetchTasks()
    }, [])
    
    return <>
        {tasks.map(t => (
            <Task key={t.id.toString()} task={t} onTasksUpdated={newTasks => setTasks(newTasks)} />
        ))}
        <NewTask onAdded={newTasks => setTasks(newTasks)}/>
    </>
}

export default TaskList