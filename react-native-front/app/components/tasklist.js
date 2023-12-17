import { useState, useEffect } from "react"
import { Text, View, TouchableOpacity } from "react-native"

import api from "../api"
import Task from "./task"
import NewTaskForm from "./newtask"
import { unsetStored } from "../storage"

const TaskList = (props) => {

    const [tasks, setTasks] = useState([])
    
    useEffect(() => {
        const fetchTasks = async () => {
            const resp = await api.get('/api/tasks')

            setTasks(resp.data)
        } 

        fetchTasks()
    }, [])

    const handlePress = async () => {
        await unsetStored('jwt_token')
        props.checkAuth()
    }
    
    return <View>
        {tasks.map(t => (
            <Task key={t.id.toString()} task={t} onTasksUpdated={newTasks => setTasks(newTasks)} />
        ))}
        <NewTaskForm onAdded={newTasks => setTasks(newTasks)}/>
        <TouchableOpacity onPress={handlePress} style={{}}>
            <Text>Logout</Text>
        </TouchableOpacity>
    </View>
}

export default TaskList