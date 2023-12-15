import { useState, useEffect } from "react"
import { Text, View } from "react-native"
import api from "../api"
import Task from "./task"
import NewTaskForm from "./newtask"

const TaskList = () => {

    const [tasks, setTasks] = useState([])
    
    useEffect(() => {
        const fetchTasks = async () => {
            const resp = await api.get('/api/tasks')

            setTasks(resp.data)
        } 

        fetchTasks()
    }, [])
    
    return <View>
        {tasks.map(t => (
            <Task key={t.id.toString()} task={t} onTasksUpdated={newTasks => setTasks(newTasks)} />
        ))}
        <NewTaskForm onAdded={newTasks => setTasks(newTasks)}/>
    </View>
}

export default TaskList